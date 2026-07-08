using System.IdentityModel.Tokens.Jwt;
using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using Wisdom.DTOs.Authentication;
using Wisdom.Entities;
using Wisdom.Repositories.Interfaces;
using Wisdom.Services.Interfaces;

namespace Wisdom.Services.Implementation;

public class RefreshTokenServices : IRefreshTokenServices
{
    // Attributes
    private readonly IRefreshTokenRepo _refreshTokenRepo;
    private readonly IJwtServices _jwtServices;
    private readonly IMapper _mapper;
    
    // Constructor
    public RefreshTokenServices(IRefreshTokenRepo refreshTokenRepo, IJwtServices jwtServices, IMapper mapper)
    {
        _refreshTokenRepo = refreshTokenRepo;
        _jwtServices = jwtServices;
        _mapper = mapper;
    }
    
    // Methods
    public static RefreshToken GenerateRefreshToken()
    {
        // Create 64 bytes randomly
        var randomBytes = new byte[64];
        using var rng = System.Security.Cryptography.RandomNumberGenerator.Create();
        rng.GetBytes(randomBytes);
        
        // Create refresh token
        return new RefreshToken
        {
            Token = Convert.ToBase64String(randomBytes),
            CreatedOn = DateTime.UtcNow,
            ExpiresOn = DateTime.UtcNow.AddHours(2)
        };
    }

    public async Task<AuthenticationDto> RefreshToken(string refreshToken)
    {
        // Get refresh token and check expiration date
        var token = await _refreshTokenRepo.Get(refreshToken);
        if (token is null || !token.IsActive) 
            return new AuthenticationDto{ IsAuthenticated = false, Message = "Invalid token."};
        
        // Revoke refresh token
        token.ExpiresOn = DateTime.UtcNow;
        await _refreshTokenRepo.Update(token);
        
        // Get user data, generate JWT, refresh token and store it
        var user = token.User;
        var jwtSecurityToken = await _jwtServices.GenerateJwtToken(user);
        var newRefreshToken = RefreshTokenServices.GenerateRefreshToken();
        newRefreshToken.UserId = user.Id;
        await _refreshTokenRepo.Add(newRefreshToken);
        
        // Map to DTO
        var authenticationDto = _mapper.Map<AuthenticationDto>(user);
        authenticationDto.IsAuthenticated = true;
        authenticationDto.Token = new JwtSecurityTokenHandler().WriteToken(jwtSecurityToken);
        authenticationDto.RefreshToken = newRefreshToken.Token;
        authenticationDto.ExpiresOn = jwtSecurityToken.ValidTo;
        
        // Return result
        return authenticationDto;
    }
}
using System.IdentityModel.Tokens.Jwt;
using AutoMapper;
using Wisdom.DTOs;
using Wisdom.DTOs.Authentication;
using Wisdom.Entities;
using Wisdom.Repositories.Interfaces;
using Wisdom.Services.Interfaces;

namespace Wisdom.Services.Implementation;

public class AuthenticationServices : IAuthenticationServices
{
    // Attributes
    private readonly IUserRepo _userRepo;
    private readonly IRefreshTokenRepo _refreshTokenRepo;
    private readonly IMapper _mapper;
    private readonly IJwtServices _jwtServices;
    private readonly ICacheServices _cacheServices;
    
    // Constructor
    public AuthenticationServices(IUserRepo userRepo, IRefreshTokenRepo refreshTokenRepo,
        IMapper mapper, IJwtServices jwtServices, ICacheServices cacheServices)
    {
        _userRepo = userRepo;
        _refreshTokenRepo = refreshTokenRepo;
        _mapper = mapper;
        _jwtServices = jwtServices;
        _cacheServices = cacheServices;
    }
    
    // Methods
    private int GenerateVerificationCode() => (new Random()).Next(100000, 1000000);
    private string HashPassword(string password) => BCrypt.Net.BCrypt.HashPassword(password);

    private bool VerifyPassword(string storedPassword, string enteredPassword)
        => BCrypt.Net.BCrypt.Verify(enteredPassword, storedPassword);
    
    public async Task<AuthenticationDto> Login(LoginDto loginDto)
    {
        // Check email existence
        var user = await _userRepo.FindByEmail(loginDto.Email);
        if (user is null)
            return new AuthenticationDto { IsAuthenticated = false, Message = "Email or password is not correct" };
        
        // Check password 
        var storedPassword = await _userRepo.GetUserPassword(loginDto.Email);
        if (!VerifyPassword(storedPassword, loginDto.Password))
            return new AuthenticationDto { IsAuthenticated = false, Message = "Email or password is not correct" };
        
        // Create JWT
        var jwtSecurityToken = await _jwtServices.GenerateJwtToken(user);
        
        // Create refresh token and save it
        var refreshToken = RefreshTokenServices.GenerateRefreshToken();
        refreshToken.UserId = user.Id;
        await _refreshTokenRepo.Add(refreshToken);
        
        // Map to DTO
        var authenticationDto = _mapper.Map<AuthenticationDto>(user);
        authenticationDto.IsAuthenticated = true;
        authenticationDto.Token = new JwtSecurityTokenHandler().WriteToken(jwtSecurityToken);
        authenticationDto.RefreshToken = refreshToken.Token;
        authenticationDto.ExpiresOn = jwtSecurityToken.ValidTo;
        
        // Return result
        return authenticationDto;
    }

    public async Task<AuthenticationDto> Register(RegisterDto registerDto)
    {
        // Check email existence
        if (await _userRepo.IsExistByEmail(registerDto.Email)) 
            return new AuthenticationDto{ IsAuthenticated = false, Message = "Error: email already exist" };
        
        // Map to entity, hash password and add user
        var user = _mapper.Map<User>(registerDto);
        user.Password = HashPassword(user.Password);
        var newUser = await _userRepo.Add(user);
        
        // Create JWT, create refresh token and add it
        var jwtSecurityToken = await _jwtServices.GenerateJwtToken(newUser);
        var refreshToken = RefreshTokenServices.GenerateRefreshToken();
        refreshToken.UserId = newUser.Id;
        await _refreshTokenRepo.Add(refreshToken);
        
        // Map to DTO
        var authenticationDto = _mapper.Map<AuthenticationDto>(newUser);
        authenticationDto.IsAuthenticated = true;
        authenticationDto.Token = new JwtSecurityTokenHandler().WriteToken(jwtSecurityToken);
        authenticationDto.RefreshToken = refreshToken.Token;
        authenticationDto.ExpiresOn = jwtSecurityToken.ValidTo;
        
        // Return result
        return authenticationDto;
    }

    public async Task<ServiceResult> Logout(string refreshToken)
    {
        // Get refresh token and check expiration date
        var token = await _refreshTokenRepo.Get(refreshToken);
        if (token is null || !token.IsActive) return new ServiceResult{ Message = "Invalid token.", StatusCode = 400 };
        
        // Revoke token
        token.RevokedOn = DateTime.UtcNow;
        await _refreshTokenRepo.Update(token);
        return new ServiceResult{ Message = "Logout done successfully.", StatusCode = 200 };
    }

    public async Task<ServiceResult> SendVerificationEmail(string email)
    {
        // Find user by email
        var user = await _userRepo.FindByEmail(email);
        if (user is null) return new ServiceResult{ Message = "Email is not exist.", StatusCode = 404 };
        
        // Generate verification code, verification email and send it
        var code = GenerateVerificationCode();
        var body = EmailServices.GenerateVerificationEmail(code);
        EmailServices.SendEmail(email, "Verify your email", body);
        
        // Save code in cache 
        _cacheServices.SaveCode(code, user.Email);
        return new ServiceResult{ Message = "You have 10 minutes for verifying your email.", StatusCode = 200 };
    }

    public async Task<ServiceResult> ResetPassword(ResetPasswordDto resetPasswordDto)
    {
        // Find user by email
        var user = await _userRepo.FindByEmail(resetPasswordDto.Email);
        if (user is null) return new ServiceResult{ Message = "Email is not exist.", StatusCode = 404 };
        
        // Get code from cache
        var storedCode = await _cacheServices.GetCode(resetPasswordDto.Email);
        
        // Check code
        if (storedCode == 0 || storedCode != resetPasswordDto.code) 
            return new ServiceResult{ Message = "Code is not correct.", StatusCode = 400 };
        
        // Update password
        user.Password = HashPassword(resetPasswordDto.Password);
        return (await _userRepo.Update(user)) 
            ? new ServiceResult{ Message = "Password changed successfully.", StatusCode = 200 }
            : new ServiceResult{ Message = "Error: something went wrong, try again later.", StatusCode = 500 };
    }
}
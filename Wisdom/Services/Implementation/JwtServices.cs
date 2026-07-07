using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using Microsoft.IdentityModel.Tokens;
using Wisdom.Configurations;
using Wisdom.Entities;
using Wisdom.Services.Interfaces;

namespace Wisdom.Services.Implementation;

public class JwtServices : IJwtServices
{
    // Attributes
    private readonly JwtConfiguration _jwtConfiguration;
    
    // Constructor
    public JwtServices(JwtConfiguration jwtConfiguration) => _jwtConfiguration = jwtConfiguration;
    
    // Methods
    public async Task<JwtSecurityToken> GenerateJwtToken(User user)
    {
        // Create claims
        var claims = new[]
        {
            new Claim(JwtRegisteredClaimNames.Jti, Guid.NewGuid().ToString()),
            new Claim(JwtRegisteredClaimNames.Sub, user.Id.ToString()),
            new Claim(JwtRegisteredClaimNames.Email, user.Email),
        };
        
        // Get Security key and determined hash algorithms
        var symmetricSecurityKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_jwtConfiguration.SigningKey));
        var signingCredentials = new SigningCredentials(symmetricSecurityKey, SecurityAlgorithms.HmacSha256);
        
        // Create jwt token
        return new JwtSecurityToken(
                issuer: _jwtConfiguration.Issuer,
                audience: _jwtConfiguration.Audience,
                claims: claims,
                signingCredentials: signingCredentials,
                expires: DateTime.UtcNow.AddHours(_jwtConfiguration.ExpiryHours)
            );
    }
}
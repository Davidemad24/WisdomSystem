using System.IdentityModel.Tokens.Jwt;
using Wisdom.Entities;

namespace Wisdom.Services.Interfaces;

public interface IJwtServices
{
    Task<JwtSecurityToken> GenerateJwtToken(User user);
}
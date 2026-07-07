using Wisdom.DTOs.Authentication;

namespace Wisdom.Services.Interfaces;

public interface IRefreshTokenServices
{
    Task<AuthenticationDto> RefreshToken(string refreshToken);
}
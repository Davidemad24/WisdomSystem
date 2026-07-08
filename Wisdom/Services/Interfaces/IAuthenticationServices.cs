using Wisdom.DTOs;
using Wisdom.DTOs.Authentication;

namespace Wisdom.Services.Interfaces;

public interface IAuthenticationServices
{
    Task<AuthenticationDto> Login(LoginDto loginDto);
    Task<AuthenticationDto> Register(RegisterDto registerDto);
    Task<ServiceResult> Logout(string refreshToken);
    Task<ServiceResult> SendVerificationEmail(string email);
    Task<ServiceResult> ResetPassword(ResetPasswordDto resetPasswordDto);
}
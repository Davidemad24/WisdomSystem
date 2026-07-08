using System.ComponentModel.DataAnnotations;
using Microsoft.AspNetCore.Mvc;
using Wisdom.DTOs.Authentication;
using Wisdom.Services.Interfaces;

namespace Wisdom.Controllers;

[ApiController]
[Route("[controller]")]
public class AuthenticationController : ControllerBase
{
    // Attributes
    private readonly IAuthenticationServices _authenticationServices;
    
    // Constructor
    public AuthenticationController(IAuthenticationServices authenticationServices)
        => _authenticationServices = authenticationServices;
    
    // Login API
    [HttpPost("Login")]
    public async Task<IActionResult> Login(LoginDto loginDto)
    {
        // Check model states
        if (!ModelState.IsValid)
            return BadRequest(ModelState);
        
        // Check login status
        var result = await _authenticationServices.Login(loginDto);
        if (!result.IsAuthenticated) return BadRequest(result.Message);
        return Ok(result);
    }
    
    // Register API
    [HttpPost("Register")]
    public async Task<IActionResult> Register(RegisterDto registerDto)
    {
        // Check model states
        if (!ModelState.IsValid)
            return BadRequest(ModelState);
        
        // Check register status
        var result = await _authenticationServices.Register(registerDto);
        if (!result.IsAuthenticated) return BadRequest(result.Message);
        return Ok(result);
    }
    
    // Logout API
    [HttpPost("Logout")]
    public async Task<IActionResult> Logout([FromBody] string refreshToken)
    {
        // Check model states
        if (!ModelState.IsValid)
            return BadRequest(ModelState);
        
        // Return status of logout
        var result = await _authenticationServices.Logout(refreshToken);
        return StatusCode(result.StatusCode, result.Message);
    }
    
    // Send verification email API
    [HttpPost("SendVerificationEmail")]
    public async Task<IActionResult> SendVerificationEmail([EmailAddress] string email)
    {
        // Check model states
        if (!ModelState.IsValid)
            return BadRequest(ModelState);
        
        // Return result
        var result = await _authenticationServices.SendVerificationEmail(email);
        return StatusCode(result.StatusCode, result.Message);
    }
    
    // Reset password API
    [HttpPatch("ResetPassword")]
    public async Task<IActionResult> ResetPassword(ResetPasswordDto resetPasswordDto)
    {
        // Check model states
        if (!ModelState.IsValid)
            return BadRequest(ModelState);
        
        // Return result
        var result = await _authenticationServices.ResetPassword(resetPasswordDto);
        return StatusCode(result.StatusCode, result.Message);
    }
}
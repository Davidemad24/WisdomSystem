using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Wisdom.Services.Interfaces;

namespace Wisdom.Controllers;

[ApiController]
[Route("[controller]")]
[AllowAnonymous]
public class RefreshTokenController : ControllerBase
{
    // Attributes
    private readonly IRefreshTokenServices _refreshTokenServices;
    
    // Constructor
    public RefreshTokenController(IRefreshTokenServices refreshTokenServices)
        => _refreshTokenServices = refreshTokenServices;
    
    // Refresh token API
    [HttpPost("RefreshToken")]
    public async Task<IActionResult> RefreshToken([FromBody] string refreshToken)
    {
        // Check model states
        if (!ModelState.IsValid)
            return BadRequest(ModelState);
        
        // Check status
        var result = await _refreshTokenServices.RefreshToken(refreshToken);
        if (!result.IsAuthenticated) return BadRequest(result.Message);
        return Ok(result);
    }
}
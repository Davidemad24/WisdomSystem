using System.ComponentModel.DataAnnotations;
using Microsoft.AspNetCore.Mvc;
using Wisdom.DTOs.Wisdom;
using Wisdom.Services.Interfaces;

namespace Wisdom.Controllers;

[ApiController]
[Route("[controller]")]
public class WisdomController : ControllerBase
{
    // Attributes
    private readonly IWisdomServices _wisdomServices;
    
    // Constructor
    public WisdomController(IWisdomServices wisdomServices) => _wisdomServices = wisdomServices;
    
    // Get user wisdom API
    [HttpGet("GetUserWisdoms")]
    public async Task<IActionResult> GetUserWisdoms([Required] int userId)
    {
        // Check model states
        if (!ModelState.IsValid)
            return BadRequest(ModelState);
        
        return Ok(await _wisdomServices.GetUserWisdoms(userId));
    }
    
    // Add wisdom API
    [HttpPost("AddWisdom")]
    public async Task<IActionResult> AddWisdom(WisdomCreationDto wisdomCreationDto)
    {
        // Check model states
        if (!ModelState.IsValid)
            return BadRequest(ModelState);
        
        // Return result
        var result = await _wisdomServices.Add(wisdomCreationDto);
        return StatusCode(result.StatusCode, result.Message);
    }
    
    // Update wisdom API
    [HttpPatch("UpdateWisdom")]
    public async Task<IActionResult> UpdateWisdom(WisdomUpdatingDto wisdomUpdatingDto)
    {
        // Check model states
        if (!ModelState.IsValid)
            return BadRequest(ModelState);
        
        // Return result
        var result = await _wisdomServices.Update(wisdomUpdatingDto);
        return StatusCode(result.StatusCode, result.Message);
    }
    
    // Delete wisdom API
    [HttpDelete("DeleteWisdom")]
    public async Task<IActionResult> DeleteWisdom(int id)
    {
        // Check model states
        if (!ModelState.IsValid)
            return BadRequest(ModelState);
        
        // Return result
        var result = await _wisdomServices.Delete(id);
        return StatusCode(result.StatusCode, result.Message);
    }
}
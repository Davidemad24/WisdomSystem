using System.ComponentModel.DataAnnotations;

namespace Wisdom.DTOs.Wisdom;

public class WisdomCreationDto
{
    [MaxLength(200, ErrorMessage = "Length for content attribute must be at most 200 characters")]
    public string Content { get; set; }
    public int UserId { get; set; }
}
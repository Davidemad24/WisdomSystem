using System.ComponentModel.DataAnnotations;

namespace Wisdom.DTOs.Authentication;

public class RegisterDto
{
    [MaxLength(50, ErrorMessage = "Length of Name attribute must be at most 50 character")]
    [RegularExpression(@"^([\p{L}]+)\s+([\p{L}]+)\s+([\p{L}]+)\s+([\p{L}]+)$", 
        ErrorMessage = "Invalid name, Enter quadruple name")]
    public string Name { get; set; }
    
    [MaxLength(150, ErrorMessage = "Length of Email attribute must be at most 150 characters")]
    [EmailAddress(ErrorMessage = "Error: invalid email.")]
    public string Email { get; set; }
    
    [StringLength(20, MinimumLength = 8
        , ErrorMessage = "Length for password attribute must be at least 8 and at most 20 characters")]
    [RegularExpression(
        @"^(?=.*[a-z])(?=.*[A-Z])(?=.*\d)(?=.*[!@#$%&*?^()_+\=-\[\]{};':""\\|,.<>\/~`]).{8,20}$",
        ErrorMessage = "Password must contain at least one capital letter, " +
                       "one small letter, one number and one  special character")]
    public string Password { get; set; }
}
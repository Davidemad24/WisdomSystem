using System.ComponentModel.DataAnnotations;

namespace Wisdom.DTOs.Authentication;

public class ResetPasswordDto
{
    [Range(100000, 1000000, ErrorMessage = "Code must be 6 digits")]
    public int Code { get; set; }
    
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
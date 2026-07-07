namespace Wisdom.DTOs.Authentication;

public class AuthenticationDto
{
    // User data
    public int Id { get; set; }
    public string Name { get; set; }
    
    // Authentication data
    public bool IsAuthenticated { get; set; }
    public string Message { get; set; }
    public string Token { get; set; }
    public string RefreshToken { get; set; }
    public DateTime ExpiresOn { get; set; }
}
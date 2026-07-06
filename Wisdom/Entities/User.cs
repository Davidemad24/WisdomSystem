namespace Wisdom.Entities;

public class User
{
    // Attributes
    public int Id { get; set; }
    public string Name { get; set; }
    public string Email { get; set; }
    public string Password { get; set; }
    
    // Relationship
    public ICollection<Wisdom> Wisdoms { get; set; }
    public ICollection<RefreshToken> RefreshTokens { get; set; }
}
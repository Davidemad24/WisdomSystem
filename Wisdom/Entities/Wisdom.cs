namespace Wisdom.Entities;

public class Wisdom
{
    // Attributes
    public int Id { get; set; }
    public string Content { get; set; }
    public DateTime CreatedOn { get; set; }
    public int UserId { get; set; }
    
    // Relationship
    public User User { get; set; }
}
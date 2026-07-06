using Microsoft.EntityFrameworkCore;
using Wisdom.Entities;

namespace Wisdom.Persistence;

public class AppDbContext : DbContext, IAppDbContext
{
    // Tables
    public DbSet<User> Users { get; set; }
    public DbSet<RefreshToken> RefreshTokens { get; set; }
    public DbSet<Entities.Wisdom> Wisdoms { get; set; }
    
    // Constructor
    public AppDbContext(DbContextOptions<AppDbContext> options) : base(options) {}
    
    // On Model Creation Method
    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        // Call onModelCreating in base 
        base.OnModelCreating(modelBuilder);
        
        // Register Fluent API configurations
        modelBuilder.ApplyConfigurationsFromAssembly(typeof(AppDbContext).Assembly);
    }
}
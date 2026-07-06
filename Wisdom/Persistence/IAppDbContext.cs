using Microsoft.EntityFrameworkCore;
using Wisdom.Entities;

namespace Wisdom.Persistence;

public interface IAppDbContext
{
    DbSet<User> Users { get; set; }
    DbSet<RefreshToken> RefreshTokens { get; set; }
    DbSet<Entities.Wisdom> Wisdoms { get; set; }
}
using Wisdom.Entities;

namespace Wisdom.Repositories.Interfaces;

public interface IRefreshTokenRepo
{
    // Queries
    Task<RefreshToken?> Get(string token);
    
    // Manipulations
    Task Add(RefreshToken refreshToken);
    Task Update(RefreshToken refreshToken);
}
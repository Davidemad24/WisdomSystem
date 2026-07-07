using Microsoft.EntityFrameworkCore;
using Wisdom.Entities;
using Wisdom.Persistence;
using Wisdom.Repositories.Interfaces;

namespace Wisdom.Repositories.Implementation;

public class RefreshTokenRepo : IRefreshTokenRepo
{
    // Attributes
    private readonly AppDbContext _appDbContext;
    
    // Constructor
    public RefreshTokenRepo(AppDbContext appDbContext) => _appDbContext = appDbContext;
    
    // Methods
    public async Task<RefreshToken?> Get(string token)
    {
        return await _appDbContext.RefreshTokens.Include(refreshToken => refreshToken.User)
            .AsNoTracking().FirstOrDefaultAsync(refreshToken => refreshToken.Token == token);
    }

    public async Task Add(RefreshToken refreshToken)
    {
        await _appDbContext.RefreshTokens.AddAsync(refreshToken);
        await _appDbContext.SaveChangesAsync();
    }

    public async Task Update(RefreshToken refreshToken)
    {
        _appDbContext.RefreshTokens.Update(refreshToken);
        await _appDbContext.SaveChangesAsync();
    }
}
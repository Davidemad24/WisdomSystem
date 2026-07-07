using Microsoft.EntityFrameworkCore;
using Wisdom.Persistence;
using Wisdom.Repositories.Interfaces;

namespace Wisdom.Repositories.Implementation;

public class WisdomRepo : IWisdomRepo
{
    // Attributes
    private readonly AppDbContext _appDbContext;
    
    // Constructor
    public WisdomRepo(AppDbContext appDbContext) => _appDbContext = appDbContext;

    // Methods
    public async Task<ICollection<Entities.Wisdom>> GetUserWisdom(int userId)
    {
        return await _appDbContext.Wisdoms.Where(wisdom => wisdom.UserId == userId).AsNoTracking().ToListAsync();
    }

    public async Task<Entities.Wisdom?> FindById(int id)
    {
        return await _appDbContext.Wisdoms.FindAsync(id);
    }

    public async Task<bool> CheckDuplication(Entities.Wisdom wisdom)
    {
        return await _appDbContext.Wisdoms.SingleOrDefaultAsync(w =>
                    w.Content == wisdom.Content && w.UserId == wisdom.UserId) != null;
    }

    public async Task<bool> Add(Entities.Wisdom wisdom)
    {
        await _appDbContext.Wisdoms.AddAsync(wisdom);
        return await _appDbContext.SaveChangesAsync() > 0;
    }

    public async Task<bool> Update(Entities.Wisdom wisdom)
    {
        _appDbContext.Wisdoms.Update(wisdom);
        return await _appDbContext.SaveChangesAsync() > 0;
    }

    public async Task<bool> Delete(Entities.Wisdom wisdom)
    {
        _appDbContext.Wisdoms.Remove(wisdom);
        return await _appDbContext.SaveChangesAsync() > 0;
    }
}
using Microsoft.EntityFrameworkCore;
using Wisdom.Entities;
using Wisdom.Persistence;
using Wisdom.Repositories.Interfaces;

namespace Wisdom.Repositories.Implementation;

public class UserRepo : IUserRepo
{
    // Attributes
    private readonly AppDbContext _appDbContext;
    
    // Constructor
    public UserRepo(AppDbContext appDbContext) => _appDbContext = appDbContext;
    
    // Methods
    public async Task<User?> FindByEmail(string email)
    {
        return await _appDbContext.Users.SingleOrDefaultAsync(user => user.Email == email);
    }

    public async Task<string?> GetUserPassword(string email)
    {
        return (await _appDbContext.Users.AsNoTracking()
                   .FirstOrDefaultAsync(user => user.Email == email))?.Password;
    }

    public async Task<bool> IsExistById(int id)
    {
        return await _appDbContext.Users.FindAsync(id) != null;
    }

    public async Task<bool> IsExistByEmail(string email)
    {
        return await _appDbContext.Users.SingleOrDefaultAsync(user => user.Email == email) is not null;
    }

    public async Task<User?> Add(User user)
    {
        return (await _appDbContext.Users.AddAsync(user)).Entity;
    }

    public async Task<bool> Update(User user)
    {
        // Check user existence
        var isUserExist = await _appDbContext.Users.FindAsync(user.Id);
        if (isUserExist == null) return false;
            
        // Update user
        var u = isUserExist;
        u.Password = user.Password;
        return await _appDbContext.SaveChangesAsync() > 0;
    }
}
using Wisdom.Entities;

namespace Wisdom.Repositories.Interfaces;

public interface IUserRepo
{
    // Queries
    Task<User?> FindByEmail(string email);
    Task<string?> GetUserPassword(string email);
    Task<bool> IsExistById(int id);
    Task<bool> IsExistByEmail(string email);
    
    // Manipulations
    Task<User?> Add(User user);
    Task<bool> Update(User user);
}
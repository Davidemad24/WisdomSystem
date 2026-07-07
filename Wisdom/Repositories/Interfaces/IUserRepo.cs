using Wisdom.Entities;

namespace Wisdom.Repositories.Interfaces;

public interface IUserRepo
{
    // Queries
    Task<User?> FindByEmail(string email);
    Task<bool> CheckPassword(string email, string password);
    Task<bool> IsExistById(int id);
    
    // Manipulations
    Task<User?> Add(User user);
    Task<bool> Update(User user);
}
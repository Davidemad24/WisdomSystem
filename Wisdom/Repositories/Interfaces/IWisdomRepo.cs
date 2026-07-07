namespace Wisdom.Repositories.Interfaces;

public interface IWisdomRepo
{
    // Queries
    Task<ICollection<Entities.Wisdom>> GetUserWisdoms(int userId);
    Task<Entities.Wisdom?> FindById(int id);
    Task<bool> CheckDuplication(Entities.Wisdom wisdom);
    
    // Manipulations
    Task<bool> Add(Entities.Wisdom wisdom);
    Task<bool> Update(Entities.Wisdom wisdom);
    Task<bool> Delete(Entities.Wisdom wisdom);
}
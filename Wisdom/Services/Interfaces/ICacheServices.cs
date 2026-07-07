namespace Wisdom.Services.Interfaces;

public interface ICacheServices
{
    void SaveCode(int code, int userId);
    Task<int> GetCode(int userId);
}
namespace Wisdom.Services.Interfaces;

public interface ICacheServices
{
    void SaveCode(int code, string email);
    Task<int> GetCode(string email);
}
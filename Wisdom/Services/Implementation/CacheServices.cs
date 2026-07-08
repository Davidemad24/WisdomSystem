using Microsoft.Extensions.Caching.Memory;
using Wisdom.Services.Interfaces;

namespace Wisdom.Services.Implementation;

public class CacheServices : ICacheServices
{
    // Attributes
    private readonly IMemoryCache _memoryCache;
    
    // Constructor
    public CacheServices(IMemoryCache memoryCache) => _memoryCache = memoryCache;
    
    // Methods
    public void SaveCode(int code, string email)
    {
        _memoryCache.Set($"Email: {email}", code, TimeSpan.FromMinutes(10));
    }

    public async Task<int> GetCode(string email)
    {
        return _memoryCache.TryGetValue($"Email: {email}", out int code) ? code : 0;
    }
}
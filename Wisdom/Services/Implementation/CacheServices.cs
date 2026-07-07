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
    public void SaveCode(int code, int userId)
    {
        _memoryCache.Set($"UserId: {userId}", code, TimeSpan.FromMinutes(10));
    }

    public async Task<int> GetCode(int userId)
    {
        return _memoryCache.TryGetValue($"UserId: {userId}", out int code) ? code : 0;
    }
}
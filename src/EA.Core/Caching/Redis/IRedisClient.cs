using EA.Core.Models.Caching.Redis;
namespace EA.Core.Caching.Redis;

public interface IRedisClient : IDisposable
{
    Task<CacheItem<T>> GetAsync<T>(string key);
    Task<IDictionary<string, CacheItem<T>>> GetAllAsync<T>(IEnumerable<string> keys);
    Task<bool> AddAsync<T>(string key, T value, TimeSpan? expiresIn = null);
    Task<bool> RemoveAsync(string key);
    Task<bool> RemoveIfEqualsAsync<T>(string key, T expected);
    Task<int> RemoveAllAsync<T>(IEnumerable<string>? keys = null);
    Task<bool> ReplaceAsync<T>(string key, T value, TimeSpan? expiresIn = null);
    Task<bool> ReplaceIfEqualsAsync<T>(string key, T value, T expected, TimeSpan? expiresIn = null);
    Task<bool> ExistsAsync(string key);
    Task<TimeSpan?> GetExprationAsync(string key);
    Task<bool> SetExprationAsync(string key, TimeSpan expiresIn);
}
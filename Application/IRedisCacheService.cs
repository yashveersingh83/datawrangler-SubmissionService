using StackExchange.Redis;
using Newtonsoft.Json;
namespace SubmissionService.Application
{
    public interface IRedisCacheService
    {
        Task SetCacheAsync<T>(string key, T value, TimeSpan? expiry = null);
        Task<T> GetCacheAsync<T>(string key);
        Task<bool> RemoveCacheAsync(string key);
        Task<bool> ExistsAsync(string key);
    }
   
}
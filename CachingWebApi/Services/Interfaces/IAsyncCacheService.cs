using CachingWebApi.Models;
namespace CachingWebApi.Services.Interfaces;

public interface IAsyncCacheService
{
    Task<CacheResult<T>> AddAsync<T>(string key, T value);
    Task<CacheResult<T>> GetAsync<T>(string key);
    Task<CacheResult<T>> RemoveAsync<T>(string key);   
    Task<bool> ExistsAsync(string key);   
}

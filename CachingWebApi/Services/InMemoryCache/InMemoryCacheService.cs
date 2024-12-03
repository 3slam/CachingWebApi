using CachingWebApi.Models;
using CachingWebApi.Services.Interfaces;
using Microsoft.Extensions.Caching.Memory;

public class InMemoryCacheService(IMemoryCache memoryCache) : ICacheService
{
    public CacheResult<T> AddAsync<T>(string key, T value)
    {
        if (string.IsNullOrWhiteSpace(key))
            return new CacheResult<T> { IsSuccess = false, Message = "Key cannot be null or empty." };

        if (value == null)
            return new CacheResult<T> { IsSuccess = false, Message = "Value cannot be null." };

        try
        {
            memoryCache.Set(key, value);
            return new CacheResult<T> { IsSuccess = true, Message = "Value added successfully." };
        }
        catch (Exception ex)
        {
            return new CacheResult<T> { IsSuccess = false, Message = $"Error adding value: {ex.Message}" };
        }
    }

    public bool ExistsAsync(string key)
    {
        if (memoryCache.TryGetValue(key, out _))
            return true;
        return false;
    }

    public CacheResult<T> GetAsync<T>(string key)
    {
        if (string.IsNullOrWhiteSpace(key))
            return new CacheResult<T> { IsSuccess = false, Message = "Key cannot be null or empty." };

        try
        {
            if (memoryCache.TryGetValue(key, out T value))
                return new CacheResult<T> { IsSuccess = true, Message = "Value retrieved successfully.", Value = value };

            return new CacheResult<T> { IsSuccess = false, Message = "Value not found in cache." };
        }
        catch (Exception ex)
        {
            return new CacheResult<T> { IsSuccess = false, Message = $"Error retrieving value: {ex.Message}" };
        }
    }

    public CacheResult<T> RemoveAsync<T>(string key)
    {
        if (string.IsNullOrWhiteSpace(key))
            return new CacheResult<T> { IsSuccess = false, Message = "Key cannot be null or empty." };

        try
        {
            memoryCache.Remove(key);
            return new CacheResult<T> { IsSuccess = true, Message = "Value removed successfully." };
        }
        catch (Exception ex)
        {
            return new CacheResult<T> { IsSuccess = false, Message = $"Error removing value: {ex.Message}" };
        }
    }
}
 
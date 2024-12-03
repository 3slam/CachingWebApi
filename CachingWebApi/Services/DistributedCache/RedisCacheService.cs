using StackExchange.Redis;
using System;
using System.Threading.Tasks;
using Newtonsoft.Json;
using Cashing.CachingService;
using CachingWebApi.Models;
using CachingWebApi.Services.Interfaces;

namespace Cashing.CachingService;

public class RedisCacheService(IConnectionMultiplexer _redis) : IAsyncCacheService
{
    private IDatabase _database => _redis.GetDatabase();
 
    public async Task<CacheResult<T>> AddAsync<T>(string key, T value)
    {
        if (string.IsNullOrWhiteSpace(key))
            return new CacheResult<T> { Message = "Key cannot be null or empty."};

        if (value == null)
            return new CacheResult<T> { Message = "Value cannot be null."};

        if(await ExistsAsync(key))
            return new CacheResult<T> { Message = "Key already exists in cache." };

        try
        {
            var serializedValue = JsonConvert.SerializeObject(value);
            bool isSet = await _database.StringSetAsync(key, serializedValue);

            return isSet
                ? new CacheResult<T> { IsSuccess = true, Message = "Value added successfully.", }
                : new CacheResult<T> { Message = "Failed to add value." };
        }
        catch (Exception ex)
        {
            return new CacheResult<T> { Message = $"Error adding value: {ex.Message}"  };
        }
    }

    public async Task<CacheResult<T>> GetAsync<T>(string key)
    {
        if (string.IsNullOrWhiteSpace(key))
            return new CacheResult<T> {  Message = "Key cannot be null or empty." };

        try
        {
            var cachedValue = await _database.StringGetAsync(key);
            if (!cachedValue.HasValue)
                return new CacheResult<T> { Message = "Value not found in cache." };

            var deserializedValue = JsonConvert.DeserializeObject<T>(cachedValue);
            return new CacheResult<T> { IsSuccess = true, Message = "Value retrieved successfully.", Value = deserializedValue };
        }
        catch (Exception ex)
        {
            return new CacheResult<T> { Message = $"Error retrieving value: {ex.Message}" };
        }
    }

    public async Task<CacheResult<T>> RemoveAsync<T>(string key)
    {
        if (string.IsNullOrWhiteSpace(key))
            return new CacheResult<T> { IsSuccess = false, Message = "Key cannot be null or empty." };

        try
        {
            bool isRemoved = await _database.KeyDeleteAsync(key);
            return isRemoved
                ? new CacheResult<T> { IsSuccess = true, Message = "Value removed successfully." }
                : new CacheResult<T> { Message = "Failed to remove value." };
        }
        catch (Exception ex)
        {
            return new CacheResult<T> { Message = $"Error removing value: {ex.Message}" };
        }
    }

    public async Task<bool> ExistsAsync(string key)
    {
        if (string.IsNullOrWhiteSpace(key))
            throw new ArgumentException("Key cannot be null or empty", nameof(key));

        try
        {
            return await _database.KeyExistsAsync(key);
        }
        catch (Exception ex)
        {
            throw new ApplicationException($"Error checking key existence: {ex.Message}", ex);
        }
    }
}

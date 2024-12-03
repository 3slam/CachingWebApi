using CachingWebApi.Models;
using Microsoft.AspNetCore.Mvc;

namespace CachingWebApi.Services.Interfaces;

public interface ICacheService   
{
     CacheResult<T> AddAsync<T>(string key, T value);
     CacheResult<T> GetAsync<T>(string key);
     CacheResult<T> RemoveAsync<T>(string key);
     bool ExistsAsync(string key);
}

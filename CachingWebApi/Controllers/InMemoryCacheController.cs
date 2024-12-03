using CachingWebApi.Services.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace CachingWebApi.Controllers;

[Route("api/[controller]")]
[ApiController]
public class InMemoryCacheController(ICacheService _inMemoryCacheService) : ControllerBase
{
    
    
    [HttpPost("add")]
    public async Task<IActionResult> AddToCache([FromQuery] string key, [FromBody] string value)
    {
        var cacheResult =  _inMemoryCacheService.AddAsync(key, value);

        if (cacheResult.IsSuccess)
            return Ok(cacheResult);

        return BadRequest(cacheResult);
    }

     
    [HttpGet("get/{key}")]
    public async Task<IActionResult> GetFromCache(string key)
    {
        var cacheResult =  _inMemoryCacheService.GetAsync<string>(key);

        if (cacheResult.IsSuccess)
            return Ok(cacheResult);

        return NotFound(cacheResult);
    }

   
    [HttpDelete("remove/{key}")]
    public async Task<IActionResult> RemoveFromCache(string key)
    {
        var cacheResult =  _inMemoryCacheService.RemoveAsync<string>(key);

        if (cacheResult.IsSuccess)
            return Ok(cacheResult);

        return BadRequest(cacheResult);
    }
 
    [HttpGet("exists/{key}")]
    public async Task<IActionResult> CheckCacheExistence(string key)
    {
        bool exists =  _inMemoryCacheService.ExistsAsync(key);
        return Ok(new { KeyExists = exists });
    }
}

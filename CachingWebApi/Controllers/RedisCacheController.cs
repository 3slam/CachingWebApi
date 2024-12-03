using CachingWebApi.Services.Interfaces;
using Cashing.CachingService;
using Microsoft.AspNetCore.Mvc;
namespace CachingWebApi.Controllers;
 
[Route("api/[controller]")]
[ApiController]
public class RedisCacheController(IAsyncCacheService redisCacheService) : ControllerBase
{ 
    [HttpPost("add")]
    public async Task<IActionResult> AddToCache([FromQuery] string key, [FromBody] string value)
    {
        var cacheResult = await redisCacheService.AddAsync(key, value);

        if (cacheResult.IsSuccess)
            return Ok(cacheResult);

        return BadRequest(cacheResult);
    }
 
    [HttpGet("get/{key}")]
    public async Task<IActionResult> GetFromCache(string key)
    {
        var cacheResult = await redisCacheService.GetAsync<string>(key);

        if (cacheResult.IsSuccess)
            return Ok(cacheResult);

        return NotFound(cacheResult);
    }

    [HttpDelete("remove/{key}")]
    public async Task<IActionResult> RemoveFromCache(string key)
    {
        var cacheResult = await redisCacheService.RemoveAsync<string>(key);

        if (cacheResult.IsSuccess)
            return Ok(cacheResult);

        return BadRequest(cacheResult);
    }

    [HttpGet("exists/{key}")]
    public async Task<IActionResult> CheckCacheExistence(string key)
    {
        bool exists = await redisCacheService.ExistsAsync(key);
        return Ok(new { KeyExists = exists });
    }
}

namespace CachingWebApi.Models;

public class CacheResult<T>
{
    public bool IsSuccess { get; set; }
    public string Message { get; set; }  
    public T? Value { get; set; }
}

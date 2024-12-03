
 
using CachingWebApi.Services.Interfaces;
using Cashing.CachingService;
using Microsoft.Extensions.Caching.Memory;
using StackExchange.Redis;

namespace CachingWebApi
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var builder = WebApplication.CreateBuilder(args);

            builder.Services.AddMemoryCache();
            var redisConnectionString = builder.Configuration.GetValue<string>("Redis:ConnectionString");
            var redisConnection = ConnectionMultiplexer.Connect(redisConnectionString);
            builder.Services.AddSingleton<IConnectionMultiplexer>(redisConnection);
            builder.Services.AddSingleton<IAsyncCacheService ,RedisCacheService>();
            builder.Services.AddSingleton<ICacheService ,InMemoryCacheService>();
          

            builder.Services.AddControllers();
            builder.Services.AddEndpointsApiExplorer();
            builder.Services.AddSwaggerGen();

            var app = builder.Build();

             
            if (app.Environment.IsDevelopment())
            {
                app.UseSwagger();
                app.UseSwaggerUI();
            }

            app.UseHttpsRedirection();

            app.UseAuthorization();


            app.MapControllers();

            app.Run();
        }
    }
}

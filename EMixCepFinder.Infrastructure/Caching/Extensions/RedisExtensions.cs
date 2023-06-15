using EMixCepFinder.Domain.Caching;
using Microsoft.Extensions.DependencyInjection;
using StackExchange.Redis;

namespace EMixCepFinder.Infrastructure.Caching.Extensions
{
    public static class RedisExtensions
    {
        public static IServiceCollection AddRedisCache(this IServiceCollection services)
        {
            services.AddScoped<ICachingService, CachingService>();
            services.AddSingleton<IConnectionMultiplexer>(sp => ConnectionMultiplexer.Connect(new ConfigurationOptions
            {
                EndPoints = { "localhost" },
                AbortOnConnectFail = false

            }));
            return services;
        }
    }
}

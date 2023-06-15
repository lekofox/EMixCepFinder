using EMixCepFinder.Domain.Caching;
using EMixCepFinder.Domain.Model;
using Microsoft.Extensions.Caching.Distributed;

namespace EMixCepFinder.Infrastructure.Caching
{
    public class CachingService : ICachingService
    {
        private readonly IDistributedCache _cache;

        public CachingService(IDistributedCache cache)
        {
            _cache = cache;
        }
        public async Task<string> GetAsync(string key)
        {
            return await _cache.GetStringAsync(key);
        }

        public async Task SetAsync(string key, string value)
        {
            await _cache.SetStringAsync(key, value);
        }
    }
}

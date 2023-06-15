using EMixCepFinder.Domain.Model;

namespace EMixCepFinder.Domain.Caching
{
    public interface ICachingService
    {
        public Task SetAsync(string key, string value);
        public Task<string> GetAsync(string key);

    }
}

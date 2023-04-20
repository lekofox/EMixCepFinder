using EMixCepFinder.Infrastructure.Database.Context;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace EMixCepFinder.Infrastructure.Database.Extensions
{
    public static class LocalDbExtensions
    {
        public static IServiceCollection AddLocalDb(this IServiceCollection services, IConfiguration configuration)
        {
            var defaultConnection = configuration["Database:LocalDb:DefaultConnection"];
            services.AddDbContext<AddressInfoContext>(options => options.UseSqlServer(defaultConnection));
            return services;
        }
    }
}

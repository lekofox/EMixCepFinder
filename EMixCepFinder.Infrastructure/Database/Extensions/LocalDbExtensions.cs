using EMixCepFinder.Infrastructure.Database.Context;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace EMixCepFinder.Infrastructure.Database.Extensions
{
    /// <summary>
    /// Provides extension methods to configure local database connection.
    /// </summary>
    public static class LocalDbExtensions
    {
        /// <summary>
        /// Adds the local database connection to the service collection.
        /// </summary>
        /// <param name="services">The <see cref="IServiceCollection"/> instance.</param>
        /// <param name="configuration">The <see cref="IConfiguration"/> instance.</param>
        /// <returns>The updated <see cref="IServiceCollection"/> instance.</returns>
        public static IServiceCollection AddLocalDb(this IServiceCollection services, IConfiguration configuration)
        {
            var defaultConnection = configuration["Database:LocalDb:DefaultConnection"];
            services.AddDbContext<AddressInfoContext>(options => options.UseSqlServer(defaultConnection));
            return services;
        }
    }
}
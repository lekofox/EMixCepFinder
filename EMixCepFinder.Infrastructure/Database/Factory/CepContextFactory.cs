using EMixCepFinder.Infrastructure.Database.Context;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Design;
using Microsoft.Extensions.Configuration;

namespace EMixCepFinder.Infrastructure.Database.Factory
{
    public class AddressInfoContextFactory : IDesignTimeDbContextFactory<AddressInfoContext>
    {
        public AddressInfoContext CreateDbContext(string[] args)
        {
            IConfiguration configuration = new ConfigurationBuilder()
            .SetBasePath(Path.Combine(Directory.GetCurrentDirectory(), "../EMixCepFinder.API"))
            .AddJsonFile("appsettings.json")
            .Build();

            var defaultConnection = configuration["Database:LocalDb:DefaultConnection"];
            var optionsBuilder = new DbContextOptionsBuilder<AddressInfoContext>();
            optionsBuilder.UseSqlServer(defaultConnection);

            return new AddressInfoContext(optionsBuilder.Options);
        }
    }
}

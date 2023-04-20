using EMixCepFinder.Domain.Model;
using Microsoft.EntityFrameworkCore;

namespace EMixCepFinder.Infrastructure.Database.Context
{
    public class AddressInfoContext : DbContext
    {
        public AddressInfoContext(DbContextOptions<AddressInfoContext> options) : base(options)
        {
        }
        public AddressInfoContext()
        {
            
        }
        public DbSet<AddressInfo> AddressInfo { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<AddressInfo>()
                .HasIndex(ai => ai.PostalCode)
                .IsUnique();
        }

    }
}


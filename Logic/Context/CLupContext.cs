using Microsoft.EntityFrameworkCore;

using Logic.Businesses;
using Logic.BusinessOwners;
using Logic.BusinessQueues;
using Logic.Users;

namespace Logic.Context
{
    public class CLupContext : DbContext, ICLupContext
    {
        public DbSet<User> Users { get; set; }

        public DbSet<BusinessOwner> BusinessOwners { get; set; }

        public DbSet<Business> Businesses { get; set; }

        public DbSet<BusinessQueue> BusinessQueues { get; set; }
        public CLupContext(DbContextOptions<CLupContext> options)
            : base(options)
        {
        }
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {

            modelBuilder.Entity<BusinessOwner>()
                        .HasMany(c => c.Businesses)
                        .WithOne(c => c.Owner);

            modelBuilder.Entity<Business>()
                        .HasMany(x => x.Queues)
                        .WithOne(x => x.Business);

        }
    }
}
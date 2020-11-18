using BC = BCrypt.Net.BCrypt;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;

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
            Database.EnsureCreated();
        }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            if (!optionsBuilder.IsConfigured)
            {
            }
        }
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {

            base.OnModelCreating(modelBuilder);

            modelBuilder.Entity<User>()
                        .HasIndex(c => c.Email)
                        .IsUnique();

            var users = new[]
            {
                new User {Id = 1, Name = "Jens", Email = "h@h.com", Password = BC.HashPassword("1234"), Zip = "3520"}
            };

            modelBuilder.Entity<User>().HasData(users);


            modelBuilder.Entity<BusinessOwner>()
                        .HasMany(c => c.Businesses);

            modelBuilder.Entity<BusinessOwner>()
                        .HasIndex(c => c.UserEmail)
                        .IsUnique();

            var owners = new[]
           {
                new BusinessOwner {Id = 1, UserEmail = "h@h.com"}

           };

            modelBuilder.Entity<BusinessOwner>().HasData(owners);

            modelBuilder.Entity<Business>()
                        .HasMany(x => x.Queues)
                        .WithOne(x => x.Business);

            var businesses = new[]
            {
                new Business {Id = 1, Name = "Cool", OwnerEmail = "h@h.com", Zip = "3520",
                             OpeningTime = 10.00, ClosingTime = 16.00, Capacity = 50},
                new Business {Id = 2, Name = "Shop", OwnerEmail = "h@h.com", Zip = "3520",
                             OpeningTime = 09.00, ClosingTime = 14.00, Capacity = 40},
                new Business {Id = 3, Name = "1337", OwnerEmail = "h@h.com", Zip = "4720",
                             OpeningTime = 08.30, ClosingTime = 15.30, Capacity = 30}
            };

            modelBuilder.Entity<Business>().HasData(businesses);

        }
    }
}
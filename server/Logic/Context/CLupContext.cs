using BC = BCrypt.Net.BCrypt;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System;

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

        public DbSet<UserQueue> UserQueues { get; set; }
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

            modelBuilder.Entity<UserQueue>().HasKey(c => new { c.UserEmail, c.BusinessQueueId });

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
                        .HasMany(x => x.Queues);

            var businesses = new[]
            {
                new Business {Id = 1, Name = "Cool", OwnerEmail = "h@h.com", Zip = "3520",
                             OpeningTime = 10.00, ClosingTime = 16.00, Capacity = 50, Type = "Supermarket"},
                new Business {Id = 2, Name = "Shop", OwnerEmail = "h@h.com", Zip = "3520",
                             OpeningTime = 09.00, ClosingTime = 14.00, Capacity = 40, Type = "Museum"},
                new Business {Id = 3, Name = "1337", OwnerEmail = "h@h.com", Zip = "4720",
                             OpeningTime = 08.30, ClosingTime = 15.30, Capacity = 30, Type = "Kiosk"}
            };

            modelBuilder.Entity<Business>().HasData(businesses);

            modelBuilder.Entity<BusinessQueue>()
                        .HasMany(b => b.Customers);


            var queues = new[]
            {
                new BusinessQueue {Id = 1, BusinessId = 1, Capacity = 50,
                                    Start = DateTime.Now.AddHours(3), End = DateTime.Now.AddHours(4),
                                    },

                new BusinessQueue {Id = 2, BusinessId = 1, Capacity = 40,
                                    Start = DateTime.Now.AddHours(4), End = DateTime.Now.AddHours(5),
                                    },

                new BusinessQueue {Id = 3, BusinessId = 1, Capacity = 30,
                                    Start = DateTime.Now.AddHours(5), End = DateTime.Now.AddHours(6),
                                    },
            };

            modelBuilder.Entity<BusinessQueue>().HasData(queues);


            // var userQueues = new[]
            // {
            //     new UserQueue {UserEmail = users[0].Email, BusinessQueueId = queues[0].Id},
            //     new UserQueue {UserEmail = users[0].Email, BusinessQueueId = queues[1].Id},
            //     new UserQueue {UserEmail = users[0].Email, BusinessQueueId = queues[2].Id}
            // };

            // modelBuilder.Entity<UserQueue>().HasData(userQueues);

        }
    }
}
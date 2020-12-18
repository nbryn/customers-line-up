using BC = BCrypt.Net.BCrypt;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System;

using Logic.Businesses;
using Logic.BusinessOwners;
using Logic.TimeSlots;
using Logic.Users;
using Logic.Bookings;

namespace Logic.Context
{
    public class CLupContext : DbContext, ICLupContext
    {
        public DbSet<User> Users { get; set; }

        public DbSet<BusinessOwner> BusinessOwners { get; set; }

        public DbSet<Business> Businesses { get; set; }

        public DbSet<TimeSlot> TimeSlots { get; set; }

        public DbSet<Booking> Bookings { get; set; }
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

            modelBuilder.Entity<Booking>().HasKey(c => new { c.UserEmail, c.TimeSlotId });

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
                        .HasMany(x => x.TimeSlots);

            modelBuilder.Entity<Business>()
                        .Property(b => b.Type)
                        .HasConversion(b => b.ToString("G"),
                        b => Enum.Parse<BusinessType>(b));


            var businesses = new[]
            {
                new Business {Id = 1, Name = "Cool", OwnerEmail = "h@h.com", Zip = 3520,
                             Opens = "10.00", Closes = "16.00", TimeSlotLength = 50, Capacity = 50, Type = BusinessType.Supermarket},
                new Business {Id = 2, Name = "Shop", OwnerEmail = "h@h.com", Zip = 3520,
                             Opens = "09.00", Closes = "14.00", TimeSlotLength = 20, Capacity = 40, Type = BusinessType.Museum},
                new Business {Id = 3, Name = "1337", OwnerEmail = "h@h.com", Zip = 4720,
                             Opens = "08.30", Closes = "15.30", TimeSlotLength = 10, Capacity = 30, Type = BusinessType.Kiosk}
            };

            modelBuilder.Entity<Business>().HasData(businesses);

            modelBuilder.Entity<TimeSlot>()
                        .HasMany(b => b.Bookings);


            var queues = new[]
            {
                new TimeSlot {Id = 1, BusinessId = 1, BusinessName = "Cool", Capacity = 50,
                                    Start = DateTime.Now.AddHours(3), End = DateTime.Now.AddHours(4),
                                    },

                new TimeSlot {Id = 2, BusinessId = 1, BusinessName = "Cool", Capacity = 40,
                                    Start = DateTime.Now.AddHours(4), End = DateTime.Now.AddHours(5),
                                    },

                new TimeSlot {Id = 3, BusinessId = 1, BusinessName = "Cool", Capacity = 30,
                                    Start = DateTime.Now.AddHours(5), End = DateTime.Now.AddHours(6),
                                    },
            };

            modelBuilder.Entity<TimeSlot>().HasData(queues);


            var bookings = new[]
            {
                new Booking {UserEmail = users[0].Email, BusinessId = 1, TimeSlotId = queues[0].Id},
                new Booking {UserEmail = users[0].Email, BusinessId = 1, TimeSlotId = queues[1].Id},
                new Booking {UserEmail = users[0].Email, BusinessId = 1, TimeSlotId = queues[2].Id}
            };

            modelBuilder.Entity<Booking>().HasData(bookings);

        }
    }
}
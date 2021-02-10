using BC = BCrypt.Net.BCrypt;
using Microsoft.EntityFrameworkCore;
using System;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

using Logic.Bookings;
using Logic.Businesses;
using Logic.BusinessOwners;
using Logic.Employees;
using Logic.TimeSlots;
using Logic.Users;

namespace Logic.Context
{
    public class CLupContext : DbContext, ICLupContext
    {
        public DbSet<Booking> Bookings { get; set; }
        public DbSet<BusinessOwner> BusinessOwners { get; set; }
        public DbSet<Business> Businesses { get; set; }
        public DbSet<Employee> Employees { get; set; }
        public DbSet<TimeSlot> TimeSlots { get; set; }
        public DbSet<User> Users { get; set; }
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

        public override Task<int> SaveChangesAsync(bool acceptAllChangesOnSuccess,
                                                   CancellationToken cancellationToken = default(CancellationToken))
        {
            var AddedEntities = ChangeTracker.Entries()
                .Where(E => E.State == EntityState.Added)
                .ToList();

            AddedEntities.ForEach(E =>
            {
                E.Property("CreatedAt").CurrentValue = DateTime.Now;
            });

            var EditedEntities = ChangeTracker.Entries()
                .Where(E => E.State == EntityState.Modified)
                .ToList();

            EditedEntities.ForEach(E =>
            {
                E.Property("UpdatedAt").CurrentValue = DateTime.Now;
            });

            return base.SaveChangesAsync(acceptAllChangesOnSuccess, cancellationToken);
        }
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {

            base.OnModelCreating(modelBuilder);

            modelBuilder.Entity<Booking>().HasKey(c => new { c.UserEmail, c.TimeSlotId });

            modelBuilder.Entity<Employee>().HasKey(e => new { e.UserEmail, e.BusinessId });

            modelBuilder.Entity<User>()
                        .HasIndex(c => c.Email)
                        .IsUnique();

            modelBuilder.Entity<User>()
                        .HasKey(u => u.Email);

            var users = new[]
            {
                new User {Id = 1, Name = "Peter", Email = "test@test.com", Password = BC.HashPassword("1234"), Zip = "3520 - Farum",
                          Address = "Farum Hovedgade 15", Longitude = 55.8122540, Latitude = 12.3706760},
                new User {Id = 2, Name = "Jens", Email = "h@h.com", Password = BC.HashPassword("1234"), Zip = "3520 - Farum",
                          Address = "Farum Hovedgade 50", Longitude = 55.810706, Latitude = 12.3640744},
                new User {Id = 3, Name = "Mads", Email = "mads@hotmail.com", Password = BC.HashPassword("1234"), Zip = "3520 - Farum",
                          Address = "Gedevasevej 15", Longitude = 55.8075915, Latitude = 12.3467888},
                new User {Id = 4, Name = "Emil", Email = "emil@live.com", Password = BC.HashPassword("1234"), Zip = "3520 - Farum",
                          Address = "Farum Hovedgade 15", Longitude = 55.8200342, Latitude = 12.3591325}
            };

            modelBuilder.Entity<User>().HasData(users);


            modelBuilder.Entity<BusinessOwner>()
                        .HasMany(c => c.Businesses);

            modelBuilder.Entity<BusinessOwner>()
                        .HasIndex(c => c.UserEmail)
                        .IsUnique();

            var owners = new[]
           {
                new BusinessOwner {Id = 1, UserEmail = "test@test.com"}
           };

            modelBuilder.Entity<BusinessOwner>().HasData(owners);

            modelBuilder.Entity<Business>()
                        .HasMany(x => x.TimeSlots);

            modelBuilder.Entity<Business>()
                        .HasMany(x => x.Employees);

            modelBuilder.Entity<Business>()
                        .Property(b => b.Type)
                        .HasConversion(b => b.ToString("G"),
                        b => Enum.Parse<BusinessType>(b));

            var businesses = new[]
            {
                new Business {Id = 1, Name = "Cool", OwnerEmail = "test@test.com", Zip = "3520 - Farum", Address = "Ryttergårdsvej 10", 
                             Longitude = 55.8137419, Latitude = 12.3935222, Opens = "10.00", Closes = "16.00", TimeSlotLength = 50, 
                             Capacity = 50, Type = BusinessType.Supermarket},
                new Business {Id = 2, Name = "Shop", OwnerEmail = "test@test.com", Zip = "3520 - Farum", Address = "Farum Hovedgade 100",
                             Longitude = 55.809127, Latitude = 12.3544073, Opens = "09.00", Closes = "14.00", TimeSlotLength = 20, 
                             Capacity = 40, Type = BusinessType.Museum},
                new Business {Id = 3, Name = "1337", OwnerEmail = "test@test.com", Zip = "2300 - København S", Address = "Vermlandsgade 30",
                                Longitude = 55.668442, Latitude = 12.5988833, Opens = "08.30", Closes = "15.30", TimeSlotLength = 10, 
                                Capacity = 30, Type = BusinessType.Kiosk}
            };

            modelBuilder.Entity<Business>().HasData(businesses);

            modelBuilder.Entity<TimeSlot>()
                        .HasMany(b => b.Bookings);


            var timeSlots = new[]
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

            modelBuilder.Entity<TimeSlot>().HasData(timeSlots);

            var bookings = new[]
            {
                new Booking {UserEmail = users[0].Email, BusinessId = 1, TimeSlotId = timeSlots[0].Id},
                new Booking {UserEmail = users[0].Email, BusinessId = 1, TimeSlotId = timeSlots[1].Id},
                new Booking {UserEmail = users[0].Email, BusinessId = 1, TimeSlotId = timeSlots[2].Id}
            };

            modelBuilder.Entity<Booking>().HasData(bookings);

            var employees = new[]
            {
                new Employee {Id = 1, CreatedAt = DateTime.Now, BusinessId = businesses[0].Id,
                              UserEmail = users[1].Email},

                new Employee {Id = 2, CreatedAt = DateTime.Now, BusinessId = businesses[0].Id,
                              UserEmail = users[2].Email}
            };

            modelBuilder.Entity<Employee>().HasData(employees);
        }
    }
}
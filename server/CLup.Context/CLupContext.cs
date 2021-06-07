using Microsoft.EntityFrameworkCore;
using System;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

using CLup.Bookings;
using CLup.Businesses;
using CLup.Employees;
using CLup.TimeSlots;
using CLup.Users;

namespace CLup.Context
{
    public class CLupContext : DbContext
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

            modelBuilder.Entity<Business>()
                       .Property(b => b.Type)
                       .HasConversion(b => b.ToString("G"),
                       b => Enum.Parse<BusinessType>(b));

            modelBuilder.Entity<User>()
                        .HasIndex(c => c.Email)
                        .IsUnique();

            modelBuilder.Entity<User>()
                        .HasKey(u => u.Email);


             modelBuilder.Entity<BusinessOwner>()
                        .HasMany(c => c.Businesses);

            modelBuilder.Entity<BusinessOwner>()
                        .HasIndex(c => c.UserEmail)
                        .IsUnique();

            modelBuilder.Entity<Business>()
                        .HasMany(x => x.TimeSlots)
                        .WithOne(x => x.Business);

            modelBuilder.Entity<Business>()
                        .HasMany(x => x.Employees)
                        .WithOne(x => x.Business);

            modelBuilder.Entity<TimeSlot>()
                        .HasMany(t => t.Bookings)
                        .WithOne(b => b.TimeSlot);
        }
    }
}
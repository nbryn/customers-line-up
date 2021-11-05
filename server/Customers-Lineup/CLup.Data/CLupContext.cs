using System;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

using MediatR;
using Microsoft.EntityFrameworkCore;

using CLup.Data.EntityConfigurations;
using CLup.Domain.Bookings;
using CLup.Domain.Businesses;
using CLup.Domain.Businesses.Employees;
using CLup.Domain.Businesses.TimeSlots;
using CLup.Domain.Users;
using CLup.Extensions;

namespace CLup.Data
{
    public class CLupContext : DbContext
    {
        public const string DEFAULT_SCHEMA = "CLup";
        public DbSet<Booking> Bookings { get; set; }
        public DbSet<BusinessOwner> BusinessOwners { get; set; }
        public DbSet<Business> Businesses { get; set; }
        public DbSet<Employee> Employees { get; set; }
        public DbSet<TimeSlot> TimeSlots { get; set; }
        public DbSet<User> Users { get; set; }
        public DbSet<BusinessMessage> BusinessMessages { get; set; }
        public DbSet<UserMessage> UserMessages { get; set; }

        private readonly IMediator _mediator;

        public CLupContext(DbContextOptions<CLupContext> options, IMediator mediator)
            : base(options)
        {
            _mediator = mediator;
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            modelBuilder.ApplyConfiguration(new BookingEntityTypeConfiguration());
            modelBuilder.ApplyConfiguration(new BusinessEntityTypeConfiguration());
            modelBuilder.ApplyConfiguration(new BusinessOwnerEntityTypeConfiguration());

            modelBuilder.ApplyConfiguration(new EmployeeEntityTypeConfiguration());
            modelBuilder.ApplyConfiguration(new TimeSlotEntityTypeConfiguration());
            modelBuilder.ApplyConfiguration(new UserEntityTypeConfiguration());

            modelBuilder.ApplyConfiguration(new BusinessMessageEntityTypeConfiguration());
            modelBuilder.ApplyConfiguration(new UserMessageEntityTypeConfiguration());
        }

        public async override Task<int> SaveChangesAsync(
            bool acceptAllChangesOnSuccess,
            CancellationToken cancellationToken = default(CancellationToken))
        {
            await _mediator.DispatchDomainEventsAsync(this);
            MarkEntitiesAsUpdated();

            return await base.SaveChangesAsync(acceptAllChangesOnSuccess, cancellationToken);
        }

        private void MarkEntitiesAsUpdated()
        {
            var AddedEntities = ChangeTracker.Entries()
                            .Where(E => E.State == EntityState.Added)
                            .ToList();

            AddedEntities.ForEach(E =>
            {
                if (E.Entity.ToString() != "ValueObject")
                {
                    E.Property("CreatedAt").CurrentValue = DateTime.Now;
                }
            });

            var EditedEntities = ChangeTracker.Entries()
                .Where(E => E.State == EntityState.Modified)
                .ToList();

            EditedEntities.ForEach(E =>
            {
                if (E.Entity.ToString() != "ValueObject")
                {
                    E.Property("UpdatedAt").CurrentValue = DateTime.Now;
                }
            });
        }
    }
}
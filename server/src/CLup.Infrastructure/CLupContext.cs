using System;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using CLup.Domain.Booking;
using MediatR;
using Microsoft.EntityFrameworkCore;
using CLup.Infrastructure.Extensions;
using CLup.Domain.Business;
using CLup.Domain.Business.Employee;
using CLup.Domain.Business.TimeSlot;
using CLup.Domain.Shared;
using CLup.Domain.User;
using CLup.Infrastructure.EntityConfigurations;

namespace CLup.Infrastructure
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
        
        public async Task<int> AddAndSave(params Entity[] entities)
        {
            foreach (var entity in entities)
            {
                Add(entity);
            }
            
            return await SaveChangesAsync();
        }

        public void CreateEntityIfNotExists<T>(T existingEntity, T newEntity) where T : Entity
        {
            if (existingEntity == null)
            {
                newEntity.Id = Guid.NewGuid().ToString(); 
                Add(newEntity);
            }
        }

        public async Task<int> RemoveAndSave(Entity value)
        {
            Remove(value);

            return await SaveChangesAsync();
        }

        public async Task<int> UpdateEntity<T>(string id, T updatedEntity) where T : Entity
        {
            var entity = (Entity)await FindAsync(typeof(T), id);

            updatedEntity.Id = entity.Id;
            Entry(entity).CurrentValues.SetValues(updatedEntity);

            return await SaveChangesAsync();
        }

        private void MarkEntitiesAsUpdated()
        {
            var addedEntities = ChangeTracker.Entries()
                            .Where(E => E.State == EntityState.Added)
                            .ToList();

            addedEntities.ForEach(E =>
            {
                if (E.Entity.ToString() != "ValueObject")
                {
                    E.Property("CreatedAt").CurrentValue = DateTime.Now;
                }
            });

            var editedEntities = ChangeTracker.Entries()
                .Where(entry => entry.State == EntityState.Modified)
                .ToList();

            editedEntities.ForEach(e =>
            {
                if (e.Entity.ToString() != "ValueObject")
                {
                    e.Property("UpdatedAt").CurrentValue = DateTime.Now;
                }
            });
        }
    }
}
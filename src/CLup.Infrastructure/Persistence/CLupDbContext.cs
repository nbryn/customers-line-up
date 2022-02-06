using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Threading;
using System.Threading.Tasks;
using CLup.Application.Shared.Interfaces;
using CLup.Domain.Bookings;
using CLup.Domain.Businesses;
using CLup.Domain.Businesses.Employees;
using CLup.Domain.Businesses.TimeSlots;
using CLup.Domain.Messages;
using CLup.Domain.Shared;
using CLup.Domain.Users;
using Microsoft.EntityFrameworkCore;

namespace CLup.Infrastructure.Persistence
{
    public class CLupDbContext : DbContext, ICLupDbContext
    {
        private readonly IDomainEventService _domainEventService;

        public const string DEFAULT_SCHEMA = "CLup";
        public DbSet<Booking> Bookings { get; set; }
        public DbSet<BusinessOwner> BusinessOwners { get; set; }
        public DbSet<Business> Businesses { get; set; }
        public DbSet<Employee> Employees { get; set; }
        public DbSet<TimeSlot> TimeSlots { get; set; }
        public DbSet<User> Users { get; set; }
        public DbSet<Message> Messages { get; set; }

        public CLupDbContext(
            DbContextOptions<CLupDbContext> options,
            IDomainEventService domainEventService)
            : base(options)
        {
            _domainEventService = domainEventService;
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.ApplyConfigurationsFromAssembly(Assembly.GetExecutingAssembly());

            base.OnModelCreating(modelBuilder);
        }

        public override async Task<int> SaveChangesAsync(
            bool acceptAllChangesOnSuccess,
            CancellationToken cancellationToken = default(CancellationToken))
        {
            MarkEntitiesAsUpdated();

            var events = ChangeTracker.Entries<IHasDomainEvent>()
                .SelectMany(x => x.Entity.DomainEvents)
                .Where(domainEvent => !domainEvent.IsPublished)
                .ToList();

            await DispatchEvents(events);

            return await base.SaveChangesAsync(acceptAllChangesOnSuccess, cancellationToken);
        }

        public async Task<int> AddAndSave(params Entity[] entities)
        {
            await base.AddRangeAsync(entities);

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
                            .Where(entry => entry.State == EntityState.Added)
                            .ToList();

            addedEntities.ForEach(entry =>
            {
                if (entry.Entity.ToString() != "ValueObject")
                {
                    entry.Property("CreatedAt").CurrentValue = DateTime.Now;
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

        private async Task DispatchEvents(IList<DomainEvent> events)
        {
            foreach (var @event in events)
            {
                @event.IsPublished = true;
                await _domainEventService.Publish(@event);
            }
        }
    }
}
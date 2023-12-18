using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Threading;
using System.Threading.Tasks;
using CLup.Application.Shared.Interfaces;
using CLup.Domain.Bookings;
using CLup.Domain.Businesses;
using CLup.Domain.Employees;
using CLup.Domain.Messages;
using CLup.Domain.Shared;
using CLup.Domain.TimeSlots;
using CLup.Domain.Users;
using Microsoft.EntityFrameworkCore;

namespace CLup.Infrastructure.Persistence
{
    public class CLupDbContext : DbContext, ICLupRepository
    {
        private readonly IDomainEventService _domainEventService;

        public const string DEFAULT_SCHEMA = "CLup";

        public DbSet<Business> Businesses { get; set; }

        public DbSet<Employee> Employees { get; set; }

        public DbSet<TimeSlot> TimeSlots { get; set; }

        public DbSet<Message> Messages { get; set; }

        public DbSet<Booking> Bookings { get; set; }

        public DbSet<User> Users { get; set; }

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

        public async Task<IList<Business>> FetchAllBusinesses()
            => await Businesses
                .Include(business => business.BookingIds)
                .ThenInclude(booking => booking.TimeSlot)
                .ThenInclude(timeSlot => timeSlot.Business)
                .Include(business => business.Bookings)
                .ThenInclude(booking => booking.User)
                .AsSplitQuery()
                .AsNoTracking()
                .ToListAsync();

        public async Task<Business> FetchBusiness(string businessId)
            => await Businesses.FirstOrDefaultAsync(business => business.Id == businessId);

        public async Task<TimeSlot> FetchTimeSlot(string timeSlotId)
            => await TimeSlots.Include(timeSlot => timeSlot.BookingIds)
                .FirstOrDefaultAsync(timeSlot => timeSlot.Id == timeSlotId);

        public async Task<User> FetchUserAggregate(string mailOrId)
            => await Users
                .Include(user => user.SentMessageIds)
                .ThenInclude(message => message.MessageData)
                .Include(user => user.SentMessages)
                .ThenInclude(message => message.Metadata)
                .Include(user => user.ReceivedMessages)
                .ThenInclude(message => message.MessageData)
                .Include(user => user.ReceivedMessages)
                .ThenInclude(message => message.Metadata)
                .Include(user => user.Bookings)
                .ThenInclude(booking => booking.Business)
                .Include(user => user.Bookings)
                .ThenInclude(booking => booking.TimeSlot)
                .ThenInclude(timeSlot => timeSlot.Business)
                .Include(user => user.Businesses)
                .ThenInclude(business => business.Bookings)
                .ThenInclude(booking => booking.TimeSlot)
                .ThenInclude(timeSlot => timeSlot.Business)
                .Include(user => user.Businesses)
                .ThenInclude(business => business.Bookings)
                .ThenInclude(booking => booking.User)
                .Include(user => user.Businesses)
                .ThenInclude(business => business.ReceivedMessages)
                .Include(user => user.Businesses)
                .ThenInclude(business => business.SentMessages)
                .Include(user => user.Businesses)
                .ThenInclude(business => business.Employees)
                .ThenInclude(employee => employee.User)
                .Include(user => user.Businesses)
                .ThenInclude(business => business.TimeSlots)
                .ThenInclude(timeSlot => timeSlot.Bookings)
                .AsSplitQuery()
                .FirstOrDefaultAsync(user => user.Id == mailOrId || user.UserData.Email == mailOrId);

        public async Task<IList<User>> FetchUsersNotEmployedByBusiness(string businessId)
        {
            var employeeIds = await Employees
                .Where(employee => employee.BusinessId == businessId)
                .Select(employee => employee.UserId)
                .ToListAsync();

            var users = await Users
                .Where(user => !employeeIds.Contains(user.Id))
                .ToListAsync();

            return users;
        }

        public override async Task<int> SaveChangesAsync(
            bool acceptAllChangesOnSuccess,
            CancellationToken cancellationToken = default)
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
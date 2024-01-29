using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Threading;
using System.Threading.Tasks;
using CLup.Application.Shared.Interfaces;
using CLup.Domain.Bookings;
using CLup.Domain.Businesses;
using CLup.Domain.Businesses.ValueObjects;
using CLup.Domain.Employees;
using CLup.Domain.Messages;
using CLup.Domain.Shared;
using CLup.Domain.TimeSlots;
using CLup.Domain.Users;
using CLup.Domain.Users.ValueObjects;
using Microsoft.EntityFrameworkCore;

namespace CLup.Infrastructure.Persistence;

public sealed class CLupDbContext : DbContext, ICLupRepository
{
    private readonly IDomainEventService _domainEventService;

    public const string DEFAULT_SCHEMA = "CLup";

    public DbSet<Business> Businesses { get; }

    public DbSet<Employee> Employees { get; }

    public DbSet<TimeSlot> TimeSlots { get; }

    public DbSet<UserMessage> UserMessages { get; }

    public DbSet<BusinessMessage> BusinessMessages { get; }

    public DbSet<Booking> Bookings { get; }

    public DbSet<User> Users { get; }

    public CLupDbContext(
        DbContextOptions<CLupDbContext> options,
        IDomainEventService domainEventService)
        : base(options)
    {
        _domainEventService = domainEventService;
        AppContext.SetSwitch("Npgsql.EnableLegacyTimestampBehavior", true);
    }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.ApplyConfigurationsFromAssembly(Assembly.GetExecutingAssembly());
        base.OnModelCreating(modelBuilder);
    }

    public async Task<IList<Business>> FetchAllBusinesses()
        => await Businesses
            .Include(business => business.Bookings)
            .ThenInclude(booking => booking.TimeSlot)
            .ThenInclude(timeSlot => timeSlot.Business)
            .Include(business => business.Bookings)
            .ThenInclude(booking => booking.User)
            .AsSplitQuery()
            .AsNoTracking()
            .ToListAsync();

    public async Task<Business?> FetchBusinessAggregate(UserId userId, BusinessId businessId)
        => await Businesses
            .Include(business => business.Bookings)
            .ThenInclude(booking => booking.TimeSlot)
            .ThenInclude(timeSlot => timeSlot.Business)
            .Include(business => business.Bookings)
            .ThenInclude(booking => booking.User)
            .AsSplitQuery()
            .FirstOrDefaultAsync(business => business.OwnerId == userId && business.Id == businessId);

    public Task<Business?> FetchBusinessById(BusinessId businessId)
        => Businesses
            .Include(business => business.TimeSlots)
            .FirstOrDefaultAsync(business => business.Id == businessId);

    public async Task<User?> FetchUserAggregate(UserId userId)
        => await Users
            .Include(user => user.SentMessages)
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
            .AsSplitQuery()
            .FirstOrDefaultAsync(user => user.Id == userId);

    public async Task<User?> FetchUserByEmail(string email)
        => await Users.FirstOrDefaultAsync(user => user.UserData.Email == email);

    public async Task<IList<User>> FetchUsersNotEmployedByBusiness(BusinessId businessId)
    {
        var employeeIds = await Employees
            .Where(employee => employee.BusinessId.Value == businessId.Value)
            .Select(employee => employee.UserId.Value)
            .ToListAsync();

        var users = await Users
            .Where(user => !employeeIds.Contains(user.Id.Value))
            .ToListAsync();

        return users;
    }

    public override async Task<int> SaveChangesAsync(CancellationToken cancellationToken = default)
    {
        MarkEntitiesAsUpdated();

        var events = ChangeTracker.Entries<IHasDomainEvent>()
            .SelectMany(x => x.Entity.DomainEvents)
            .Where(domainEvent => !domainEvent.IsPublished)
            .ToList();

        await DispatchEvents(events);

        return await base.SaveChangesAsync(true, cancellationToken);
    }

    public async Task<int> AddAndSave(CancellationToken cancellationToken, params Entity[] entities)
    {
        await AddRangeAsync(entities);

        return await SaveChangesAsync(true);
    }

    public async Task<int> RemoveAndSave(Entity value, CancellationToken cancellationToken)
    {
        Remove(value);

        return await SaveChangesAsync(true, cancellationToken);
    }

    public async Task<int> UpdateEntity(Guid id, Entity updatedEntity, CancellationToken cancellationToken)
    {
        var entity = (Entity)await FindAsync(typeof(Entity), id);

        // Is this necessary?
        // Use reflection
        // updatedEntity.Id = entity.Id;
        Entry(entity).CurrentValues.SetValues(updatedEntity);

        return await SaveChangesAsync(cancellationToken);
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

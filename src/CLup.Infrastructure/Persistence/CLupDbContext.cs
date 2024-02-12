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
using CLup.Domain.Shared.ValueObjects;
using CLup.Domain.TimeSlots;
using CLup.Domain.Users;
using CLup.Domain.Users.ValueObjects;
using Microsoft.EntityFrameworkCore;

namespace CLup.Infrastructure.Persistence;

public sealed class CLupDbContext : DbContext, ICLupRepository
{
    private readonly IDomainEventService _domainEventService;

    public const string DEFAULT_SCHEMA = "CLup";

    public DbSet<Business> Businesses { get; private set; }

    public DbSet<Employee> Employees { get; private set; }

    public DbSet<TimeSlot> TimeSlots { get; private set; }

    public DbSet<UserMessage> UserMessages { get; private set; }

    public DbSet<BusinessMessage> BusinessMessages { get; private set; }

    public DbSet<Booking> Bookings { get; private set; }

    public DbSet<User> Users { get; private set; }

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
            .ThenInclude(booking => booking.User)
            .Include(business => business.TimeSlots)
            .Include(business => business.Employees)
            .Include(business => business.SentMessages)
            .Include(business => business.ReceivedMessages)
            .AsSplitQuery()
            .AsNoTracking()
            .ToListAsync();

    public async Task<Business?> FetchBusinessAggregate(UserId userId, BusinessId businessId)
        => await Businesses
            .Include(business => business.Bookings)
            .ThenInclude(booking => booking.User)
            .Include(business => business.Employees)
            .ThenInclude(employee => employee.User)
            .Include(business => business.TimeSlots)
            .Include(user => user.SentMessages)
            .ThenInclude(message => message.Metadata)
            .Include(user => user.ReceivedMessages)
            .ThenInclude(message => message.Sender)
            .Include(user => user.ReceivedMessages)
            .ThenInclude(message => message.Metadata)
            .AsSplitQuery()
            .FirstOrDefaultAsync(business => business.OwnerId == userId && business.Id == businessId);

    public async Task<Business?> FetchBusinessById(BusinessId businessId)
        => await Businesses
            .Include(business => business.TimeSlots)
            .ThenInclude(timeSlot => timeSlot.Bookings)
            .FirstOrDefaultAsync(business => business.Id == businessId);

    public async Task<IList<Business>> FetchBusinessesByOwner(UserId ownerId)
        => await Businesses
            .Where(business => business.OwnerId == ownerId)
            .ToListAsync();

    public async Task<User?> FetchUserAggregate(UserId userId)
        => await Users
            .Include(user => user.Bookings)
            .ThenInclude(booking => booking.Business)
            .Include(user => user.Bookings)
            .ThenInclude(booking => booking.TimeSlot)
            .ThenInclude(timeSlot => timeSlot.Business)
            .Include(user => user.SentMessages)
            .ThenInclude(message => message.Metadata)
            .Include(user => user.ReceivedMessages)
            .ThenInclude(message => message.Sender)
            .Include(user => user.ReceivedMessages)
            .ThenInclude(message => message.Metadata)
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

    public override async Task<int> SaveChangesAsync(bool acceptChanges = true,
        CancellationToken cancellationToken = default)
    {
        MarkEntitiesAsUpdated();

        var events = ChangeTracker.Entries<IHasDomainEvent>()
            .SelectMany(x => x.Entity.DomainEvents)
            .Where(domainEvent => !domainEvent.IsPublished)
            .ToList();

        await DispatchEvents(events);

        return await base.SaveChangesAsync(acceptChanges, cancellationToken);
    }

    public async Task<int> AddAndSave(CancellationToken cancellationToken, params Entity[] entities)
    {
        await AddRangeAsync(entities);

        return await SaveChangesAsync(true, cancellationToken);
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

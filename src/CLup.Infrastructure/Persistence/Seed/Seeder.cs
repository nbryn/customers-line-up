using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using CLup.Domain.Businesses;
using CLup.Domain.Businesses.Enums;
using CLup.Domain.Businesses.ValueObjects;
using CLup.Domain.TimeSlots.ValueObjects;
using CLup.Domain.Users.Enums;
using CLup.Domain.Users.ValueObjects;
using CLup.Infrastructure.Persistence.Seed.Builders;
using BC = BCrypt.Net.BCrypt;

namespace CLup.Infrastructure.Persistence.Seed;

public interface ISeeder
{
    public Task Seed();
}

public sealed class Seeder : ISeeder
{
    private readonly CLupDbContext _dbContext;

    public Seeder(CLupDbContext dbContext)
    {
        _dbContext = dbContext;
    }

    public async Task Seed()
    {
        var userIds = AddUsers();
        var businesses = AddBusinesses(userIds);
        var businessTimeSlots = AddTimeSlots(businesses);
        AddBookings(businessTimeSlots, businesses.Select(business => business.Id).ToList(), userIds);
        AddEmployees(businesses.Select(business => business.Id).ToList(), userIds);

        await _dbContext.SaveChangesAsync();
    }

    private IList<UserId> AddUsers()
    {
        var ids = new List<UserId>();
        if (_dbContext.Users.Any())
        {
            return ids;
        }

        var user1 = new UserBuilder()
            .WithUserData("Peter", "test@test.com", BC.HashPassword("1234"))
            .WithAddress("Farum Hovedgade 15", "3520", "Farum")
            .WithCoords(55.8122540, 12.3706760)
            .WithRole(Role.Owner)
            .Build();

        _dbContext.Add(user1);
        ids.Add(user1.Id);

        var user2 = new UserBuilder()
            .WithUserData("Jens", "h@h.com", BC.HashPassword("1234"))
            .WithAddress("Farum Hovedgade 50", "3520", "Farum")
            .WithCoords(55.810706, 12.3640744)
            .WithRole(Role.Owner)
            .Build();

        _dbContext.Add(user2);
        ids.Add(user2.Id);

        var user3 = new UserBuilder()
            .WithUserData("Mads", "mads@hotmail.com", BC.HashPassword("1234"))
            .WithAddress("Gedevasevej 15", "3520", "Farum")
            .WithCoords(55.8075915, 12.3467888)
            .Build();

        _dbContext.Add(user3);
        ids.Add(user3.Id);

        var user4 = new UserBuilder()
            .WithUserData("Emil", "emil@live.com", BC.HashPassword("1234"))
            .WithAddress("Farum Hovedgade 20", "3520", "Farum")
            .WithCoords(55.8200342, 12.3591325)
            .Build();

        _dbContext.Add(user4);
        ids.Add(user4.Id);

        var user5 = new UserBuilder()
            .WithUserData("Niels", "niels@google.com", BC.HashPassword("1234"))
            .WithAddress("Kongevejen 466", "2840", "Holte")
            .WithCoords(55.825272, 12.463181)
            .Build();

        _dbContext.Add(user5);
        ids.Add(user5.Id);

        var user6 = new UserBuilder()
            .WithUserData("Kasper", "kasper@yahoo.com", BC.HashPassword("1234"))
            .WithAddress("Holte Midtpunkt 30", "2840", "Holte")
            .WithCoords(55.81111, 12.471199)
            .Build();

        _dbContext.Add(user6);
        ids.Add(user6.Id);

        return ids;
    }

    private IList<Business> AddBusinesses(IList<UserId> userIds)
    {
        var businesses = new List<Business>();
        if (_dbContext.Businesses.Any())
        {
            return businesses;
        }

        var business1 = new BusinessBuilder()
            .WithOwner(userIds[0])
            .WithBusinessData("Super Brugsen", 50, 30)
            .WithBusinessHours(10.00, 22.00)
            .WithAddress("Ryttergårdsvej 10", "3520", "Farum")
            .WithCoords(55.8137419, 12.3935222)
            .WithType(BusinessType.Supermarket)
            .Build();

        businesses.Add(business1);

        var business2 = new BusinessBuilder()
            .WithOwner(userIds[0])
            .WithBusinessData("Farum Museum", 40, 60)
            .WithBusinessHours(10.00, 16.00)
            .WithAddress("Farum Hovedgade 100", "3520", "Farum")
            .WithCoords(55.809127, 12.3544073)
            .WithType(BusinessType.Museum)
            .Build();

        businesses.Add(business2);

        var business3 = new BusinessBuilder()
            .WithOwner(userIds[0])
            .WithBusinessData("Kvick Kiosk", 30, 10)
            .WithBusinessHours(08.30, 23.00)
            .WithAddress("Vermlandsgade 30", "2300", "København S")
            .WithCoords(55.668442, 12.5988833)
            .WithType(BusinessType.Kiosk)
            .Build();

        businesses.Add(business3);

        var business4 = new BusinessBuilder()
            .WithOwner(userIds[1])
            .WithBusinessData("HairCut", 2, 40)
            .WithBusinessHours(9.00, 18.00)
            .WithAddress("Amagerbrogade 32", "2840", "Holte")
            .WithCoords(55.8191785, 12.4682046)
            .WithType(BusinessType.Hairdresser)
            .Build();

        businesses.Add(business4);

        var business5 = new BusinessBuilder()
            .WithOwner(userIds[1])
            .WithBusinessData("Odense Bibliotek", 75, 80)
            .WithBusinessHours(08.00, 20.00)
            .WithAddress("Østre Stationsvej 15", "5000", "Odense")
            .WithCoords(55.40116, 10.388635)
            .WithType(BusinessType.Library)
            .Build();

        businesses.Add(business5);

        var business6 = new BusinessBuilder()
            .WithOwner(userIds[1])
            .WithBusinessData("Foto", 8, 15)
            .WithBusinessHours(10.30, 19.00)
            .WithAddress("Nord Vej 92", "2605", "Brøndby")
            .WithCoords(55.6678010, 12.4282140)
            .WithType(BusinessType.Other)
            .Build();

        businesses.Add(business6);

        var business7 = new BusinessBuilder()
            .WithOwner(userIds[1])
            .WithBusinessData("Netto", 40, 20)
            .WithBusinessHours(09.00, 14.00)
            .WithAddress("Lundebjerg 1", "2740", "Skovlunde")
            .WithCoords(55.720478, 12.4016771)
            .WithType(BusinessType.Supermarket)
            .Build();

        businesses.Add(business7);

        var business8 = new BusinessBuilder()
            .WithOwner(userIds[1])
            .WithBusinessData("Grab'n'Go", 5, 5)
            .WithBusinessHours(08.30, 15.30)
            .WithAddress("Ellemosevej 5", "8370", "Hadsten")
            .WithCoords(56.3306771, 10.0554608)
            .WithType(BusinessType.Kiosk)
            .Build();

        businesses.Add(business8);
        _dbContext.AddRange(businesses);

        return businesses;
    }

    private Dictionary<BusinessId, List<TimeSlotId>> AddTimeSlots(IList<Business> businesses)
    {
        var businessTimeSlots = new Dictionary<BusinessId, List<TimeSlotId>>();
        if (_dbContext.TimeSlots.Any())
        {
            return businessTimeSlots;
        }

        foreach (var business in businesses)
        {
            business.GenerateTimeSlots(DateOnly.FromDateTime(DateTime.Today.AddDays(1)));
            businessTimeSlots.Add(business.Id, business.TimeSlots.Select(timeSlot => timeSlot.Id).ToList());
        }

        return businessTimeSlots;
    }

    private void AddBookings(
        IReadOnlyDictionary<BusinessId, List<TimeSlotId>> businessTimeSlots,
        IList<BusinessId> businessIds,
        IList<UserId> userIds)
    {
        if (_dbContext.Bookings.Any())
        {
            return;
        }

        _dbContext.Add(BookingCreator.Create(userIds[0], businessIds[0], businessTimeSlots[businessIds[0]].First()));
        _dbContext.Add(BookingCreator.Create(userIds[0], businessIds[1], businessTimeSlots[businessIds[1]][3]));
        _dbContext.Add(BookingCreator.Create(userIds[0], businessIds[2], businessTimeSlots[businessIds[2]][5]));
        _dbContext.Add(BookingCreator.Create(userIds[0], businessIds[3], businessTimeSlots[businessIds[3]][10]));
        _dbContext.Add(BookingCreator.Create(userIds[0], businessIds[4], businessTimeSlots[businessIds[4]][7]));
        _dbContext.Add(BookingCreator.Create(userIds[0], businessIds[5], businessTimeSlots[businessIds[5]][8]));

        foreach (var businessId in businessIds)
        {
            for (var i = 1; i < 6; i++)
            {
                _dbContext.Add(BookingCreator.Create(userIds[i], businessId, businessTimeSlots[businessId][i]));
            }
        }
    }

    private void AddEmployees(IList<BusinessId> businessIds, IList<UserId> userIds)
    {
        if (_dbContext.Employees.Any())
        {
            return;
        }

        _dbContext.Add(EmployeeCreator.Create(businessIds[0], userIds[0], "info@brugsen.dk"));
        _dbContext.Add(EmployeeCreator.Create(businessIds[0], userIds[1], "hej@brugsen.dk"));
        _dbContext.Add(EmployeeCreator.Create(businessIds[1], userIds[2], "info@farum.dk"));
        _dbContext.Add(EmployeeCreator.Create(businessIds[1], userIds[3], "hej@farum.dk"));
        _dbContext.Add(EmployeeCreator.Create(businessIds[2], userIds[4], "info@kiosk.dk"));
        _dbContext.Add(EmployeeCreator.Create(businessIds[2], userIds[5], "hej@kiosk.dk"));
    }
}

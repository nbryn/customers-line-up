using System;
using System.Collections.Generic;
using System.Linq;

using BC = BCrypt.Net.BCrypt;

using CLup.Data.Initializer.DataCreators;
using CLup.Domain;
using CLup.Domain.ValueObjects;

using TimeSpan = CLup.Domain.ValueObjects.TimeSpan;

namespace CLup.Data.Initializer
{
    public class DataInitializer
    {
        private readonly CLupContext _context;

        public DataInitializer(CLupContext context) => _context = context;
        
        public void InitializeSeed()
        {
            var userGuids = GuidHelper(4);
            var businessGuids = GuidHelper(4);
            var timeSlotGuids = GuidHelper(3);
            
            AddUsers(userGuids);
            AddBusinesses(businessGuids);
            AddTimeSlots(timeSlotGuids, businessGuids[0]);
            AddBookings(businessGuids, timeSlotGuids, userGuids);
            AddEmployees(businessGuids, userGuids);

            _context.SaveChanges();
        }
        private void AddUsers(List<string> ids)
        {
            if (_context.Users.Any())
            {
                return;
            }

            _context.Add(UserCreator.Create(ids[0], new UserData("Peter", "test@test.com", BC.HashPassword("1234")), 
                                            new Address("Farum Hovedgade 15", "3520", "Farum"), new Coords(55.8122540, 12.3706760)));

            _context.Add(UserCreator.Create(ids[1], new UserData("Jens", "h@h.com", BC.HashPassword("1234")), 
                                            new Address("Farum Hovedgade 50", "3520", "Farum"), new Coords(55.810706, 12.3640744)));

            _context.Add(UserCreator.Create(ids[2], new UserData("Mads", "mads@hotmail.com", BC.HashPassword("1234")), 
                                            new Address("Gedevasevej 15", "3520", "Farum"), new Coords(55.8075915, 12.3467888)));

            _context.Add(UserCreator.Create(ids[3], new UserData("Emil", "emil@live.com", BC.HashPassword("1234")), 
                                            new Address("Farum Hovedgade 15", "3520", "Farum"), new Coords(55.8200342, 12.3591325)));
        }
        private void AddBusinesses(List<string> ids)
        {
            if (_context.Businesses.Any())
            {
                return;
            }

            _context.Add(BusinessCreator.Create(ids[0], "test@test.com", new BusinessData("Super Brugsen", 50, 30), new TimeSpan("10.00", "16.00"),
                                                new Address("Ryttergårdsvej 10", "3520", "Farum"), new Coords(55.8137419, 12.3935222), BusinessType.Supermarket));

            _context.Add(BusinessCreator.Create(ids[1], "test@test.com", new BusinessData("Farum Museum", 40, 20), new TimeSpan("09.00", "14.00"), 
                                                new Address("Farum Hovedgade 100", "3520", "Farum"), new Coords(55.809127, 12.3544073), BusinessType.Museum));

            _context.Add(BusinessCreator.Create(ids[2], "test@test.com", new BusinessData("Kvick Kiosk", 30, 10), new TimeSpan("08.30", "15.30"),
                                                new Address("Vermlandsgade 30", "2300", "København S"), new Coords(55.668442, 12.5988833), BusinessType.Kiosk));

            _context.Add(BusinessCreator.CreateOwner(ids[3], "test@test.com"));
        }

        private void AddTimeSlots(List<string> ids, string businessId)
        {
            if (_context.TimeSlots.Any())
            {
                return;
            }

            _context.Add(TimeSlotCreator.Create(ids[0], businessId, "Super Brugsen", 50, DateTime.Now.AddHours(3), DateTime.Now.AddHours(4)));
            _context.Add(TimeSlotCreator.Create(ids[1], businessId, "Super Brugsen", 50, DateTime.Now.AddHours(4), DateTime.Now.AddHours(5)));
            _context.Add(TimeSlotCreator.Create(ids[2], businessId, "Super Brugsen", 50, DateTime.Now.AddHours(5), DateTime.Now.AddHours(6)));

        }
        private void AddBookings(List<string> businessIds, List<string> timeSlotIds, List<string> userIds)
        {
            if (_context.Bookings.Any())
            {
                return;
            }

            _context.Add(BookingCreator.Create(userIds[0], businessIds[0], timeSlotIds[0]));
            _context.Add(BookingCreator.Create(userIds[0], businessIds[0], timeSlotIds[1]));
            _context.Add(BookingCreator.Create(userIds[0], businessIds[0], timeSlotIds[2]));
        }
        private void AddEmployees(List<string> businessIds, List<string> userIds)
        {
            if (_context.Employees.Any())
            {
                return;
            }

            _context.Add(EmployeeCreator.Create(DateTime.Now, businessIds[0], userIds[0], "info@brugsen.dk"));
            _context.Add(EmployeeCreator.Create(DateTime.Now, businessIds[1], userIds[1], "info@farum.dk"));
        }

        private List<string> GuidHelper(int amount)
        {
            List<string> guids = new List<string>();

            for (int i = 0; i < amount; i++)
            {
                guids.Add(Guid.NewGuid().ToString());
            }

            return guids;
        }

    }
}
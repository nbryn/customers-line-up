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
            var userIds = AddUsers();
            var businessIds = AddBusinesses();
            var timeSlotIds = AddTimeSlots(businessIds);
            AddBookings(businessIds, timeSlotIds, userIds);
            AddEmployees(businessIds, userIds);

            _context.SaveChanges();
        }
        
        private IList<string> AddUsers()
        {
            var ids = new List<string>();
            if (_context.Users.Any())
            {
                return ids;
            }

            var user1 = UserCreator.Create(new UserData("Peter", "test@test.com", BC.HashPassword("1234")), 
                                          new Address("Farum Hovedgade 15", "3520", "Farum"), new Coords(55.8122540, 12.3706760));
            _context.Add(user1);
            ids.Add(user1.Id);

            var user2 = UserCreator.Create(new UserData("Jens", "h@h.com", BC.HashPassword("1234")), 
                                           new Address("Farum Hovedgade 50", "3520", "Farum"), new Coords(55.810706, 12.3640744));
            _context.Add(user2);
            ids.Add(user2.Id);

            var user3 = UserCreator.Create(new UserData("Mads", "mads@hotmail.com", BC.HashPassword("1234")), 
                                           new Address("Gedevasevej 15", "3520", "Farum"), new Coords(55.8075915, 12.3467888));
            _context.Add(user3);
            ids.Add(user3.Id);

            var user4 = UserCreator.Create(new UserData("Emil", "emil@live.com", BC.HashPassword("1234")), 
                                            new Address("Farum Hovedgade 15", "3520", "Farum"), new Coords(55.8200342, 12.3591325));

            _context.Add(user4);
            ids.Add(user4.Id);

            return ids;
        }

        private IList<string> AddBusinesses()
        {
            var ids = new List<string>();
            if (_context.Businesses.Any())
            {
                return ids;
            }
                 
            var business = BusinessCreator.Create("test@test.com", new BusinessData("Super Brugsen", 50, 30), new TimeSpan("10.00", "16.00"),
                                                 new Address("Ryttergårdsvej 10", "3520", "Farum"), new Coords(55.8137419, 12.3935222), BusinessType.Supermarket);
            _context.Add(business);
            ids.Add(business.Id);

           var business2 = BusinessCreator.Create("test@test.com", new BusinessData("Farum Museum", 40, 20), new TimeSpan("09.00", "14.00"), 
                                                 new Address("Farum Hovedgade 100", "3520", "Farum"), new Coords(55.809127, 12.3544073), BusinessType.Museum);
            _context.Add(business2);
            ids.Add(business2.Id);        


           var business3 = BusinessCreator.Create("test@test.com", new BusinessData("Kvick Kiosk", 30, 10), new TimeSpan("08.30", "15.30"),
                                                 new Address("Vermlandsgade 30", "2300", "København S"), new Coords(55.668442, 12.5988833), BusinessType.Kiosk);
            _context.Add(business3);
            ids.Add(business3.Id);

            _context.Add(new BusinessOwner("test@test.com"));

            return ids;
        }

        private IList<string> AddTimeSlots(IList<string> businessIds)
        {
            var ids = new List<string>();
            if (_context.TimeSlots.Any())
            {
                return ids;
            }

            var timesSLot1 = TimeSlotCreator.Create(businessIds[0], "Super Brugsen", 50, DateTime.Now.AddHours(3), DateTime.Now.AddHours(4));

            _context.Add(timesSLot1);
            ids.Add(timesSLot1.Id);

            var timeSlot2 = TimeSlotCreator.Create(businessIds[0], "Super Brugsen", 50, DateTime.Now.AddHours(4), DateTime.Now.AddHours(5));

            _context.Add(timeSlot2);
            ids.Add(timeSlot2.Id);

            var timeSlot3 = TimeSlotCreator.Create(businessIds[0], "Super Brugsen", 50, DateTime.Now.AddHours(5), DateTime.Now.AddHours(6));

            _context.Add(timeSlot3);
            ids.Add(timeSlot3.Id);

            return ids;
        }

        private void AddBookings(IList<string> businessIds, IList<string> timeSlotIds, IList<string> userIds)
        {
            if (_context.Bookings.Any())
            {
                return;
            }

            _context.Add(BookingCreator.Create(userIds[0], businessIds[0], timeSlotIds[0]));
            _context.Add(BookingCreator.Create(userIds[0], businessIds[0], timeSlotIds[1]));
            _context.Add(BookingCreator.Create(userIds[0], businessIds[0], timeSlotIds[2]));
        }

        private void AddEmployees(IList<string> businessIds, IList<string> userIds)
        {
            if (_context.Employees.Any())
            {
                return;
            }

            _context.Add(EmployeeCreator.Create(businessIds[0], userIds[0], "info@brugsen.dk"));
            _context.Add(EmployeeCreator.Create(businessIds[1], userIds[1], "info@farum.dk"));
        }
    }
}
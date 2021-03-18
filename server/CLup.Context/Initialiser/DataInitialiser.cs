using BC = BCrypt.Net.BCrypt;
using System;

using CLup.Businesses;
using CLup.Context.Initialiser.DataCreators;

namespace CLup.Context.Initialiser
{
    public class DataInitialiser
    {
        private readonly ICLupContext _context;

        public DataInitialiser(ICLupContext context)
        {
            _context = context;
        }

        public void InitialiseSeed()
        {
            AddUsers();
            AddBusinesses();
            AddTimeSlots();
            AddBookings();
            AddEmployees();
            _context.SaveChangesAsync();
        }

        public void InitialiseSample()
        {
        }

        private void AddUsers()
        {
            _context.Users.Add(UserCreator.Create(1, "Peter", "test@test.com", BC.HashPassword("1234"), "3520 - Farum",
                          "Farum Hovedgade 15", 55.8122540, 12.3706760));

            _context.Users.Add(UserCreator.Create(2, "Jens", "h@h.com", BC.HashPassword("1234"), "3520 - Farum",
                         "Farum Hovedgade 50", 55.810706, 12.3640744));

            _context.Users.Add(UserCreator.Create(3, "Mads", "mads@hotmail.com", BC.HashPassword("1234"), "3520 - Farum",
                          "Gedevasevej 15", 55.8075915, 12.3467888));

            _context.Users.Add(UserCreator.Create(4, "Emil", "emil@live.com", BC.HashPassword("1234"), "3520 - Farum",
                          "Farum Hovedgade 15", 55.8200342, 12.3591325));
        }
        private void AddBusinesses()
        {
            _context.Businesses.Add(BusinessCreator.Create(1, "Cool", "test@test.com", "Ryttergårdsvej 10", "3520 - Farum",
                                         55.8137419, 12.3935222, 50, "10.00", "16.00", 30,
                                         BusinessType.Supermarket));

            _context.Businesses.Add(BusinessCreator.Create(2, "Shop", "test@test.com", "Farum Hovedgade 100", "3520 - Farum",
                                         55.809127, 12.3544073, 40, "09.00", "14.00", 20,
                                        BusinessType.Museum));

            _context.Businesses.Add(BusinessCreator.Create(3, "1337", "test@test.com", "Vermlandsgade 30", "2300 - København S",
                                55.668442, 12.5988833, 30, "08.30", "15.30", 10,
                                  BusinessType.Kiosk));

            _context.BusinessOwners.Add(BusinessCreator.CreateOwner(1, "test@test.com"));
        }

        private void AddTimeSlots()
        {
            _context.TimeSlots.Add(TimeSlotCreator.Create(1, 1, "Cool", 50, DateTime.Now.AddHours(3), DateTime.Now.AddHours(4)));
            _context.TimeSlots.Add(TimeSlotCreator.Create(2, 1, "Cool", 50, DateTime.Now.AddHours(4), DateTime.Now.AddHours(5)));
            _context.TimeSlots.Add(TimeSlotCreator.Create(3, 1, "Cool", 50, DateTime.Now.AddHours(5), DateTime.Now.AddHours(6)));

        }
        private void AddBookings()
        {
            _context.Bookings.Add(BookingCreator.Create("test@test.com", 1, 1));
            _context.Bookings.Add(BookingCreator.Create("test@test.com", 1, 2));
            _context.Bookings.Add(BookingCreator.Create("test@test.com", 1, 3));
        }
        private void AddEmployees()
        {
            _context.Employees.Add(EmployeeCreator.Create(1, DateTime.Now, 1, "test@test.com"));

            _context.Employees.Add(EmployeeCreator.Create(2, DateTime.Now, 1, "test@test.com"));

        }

    }
}
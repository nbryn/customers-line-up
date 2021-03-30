using BC = BCrypt.Net.BCrypt;
using System;
using System.Linq;

using CLup.Businesses;
using CLup.Context.Initialiser.DataCreators;
namespace CLup.Context.Initialiser
{
    public class DataInitialiser
    {
        private readonly CLupContext _context;

        public DataInitialiser(CLupContext context)
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
            AddBusinessOwners();
            _context.SaveChanges();
        }
        private void AddUsers()
        {
            if (_context.Users.Any())
            {
                return;
            }

            _context.Add(UserCreator.Create("Peter", "test@test.com", BC.HashPassword("1234"), "3520 - Farum",
                          "Farum Hovedgade 15", 55.8122540, 12.3706760));

            _context.Add(UserCreator.Create("Jens", "h@h.com", BC.HashPassword("1234"), "3520 - Farum",
                        "Farum Hovedgade 50", 55.810706, 12.3640744));

            _context.Add(UserCreator.Create("Mads", "mads@hotmail.com", BC.HashPassword("1234"), "3520 - Farum",
                          "Gedevasevej 15", 55.8075915, 12.3467888));

            _context.Add(UserCreator.Create("Emil", "emil@live.com", BC.HashPassword("1234"), "3520 - Farum",
                          "Farum Hovedgade 15", 55.8200342, 12.3591325));
        }
        private void AddBusinesses()
        {
            if (_context.Businesses.Any())
            {
                return;
            }

            _context.Add(BusinessCreator.Create("Cool", "test@test.com", "Ryttergårdsvej 10", "3520 - Farum",
                                         55.8137419, 12.3935222, 50, "10.00", "16.00", 30,
                                         BusinessType.Supermarket));

            _context.Add(BusinessCreator.Create("Shop", "test@test.com", "Farum Hovedgade 100", "3520 - Farum",
                                         55.809127, 12.3544073, 40, "09.00", "14.00", 20,
                                        BusinessType.Museum));

            _context.Add(BusinessCreator.Create("1337", "test@test.com", "Vermlandsgade 30", "2300 - København S",
                                55.668442, 12.5988833, 30, "08.30", "15.30", 10,
                                  BusinessType.Kiosk));

            _context.Add(BusinessCreator.CreateOwner("test@test.com"));
        }

        private void AddTimeSlots()
        {
            if (_context.TimeSlots.Any())
            {
                return;
            }

            _context.Add(TimeSlotCreator.Create(1, "Cool", 50, DateTime.Now.AddHours(3), DateTime.Now.AddHours(4)));
            _context.Add(TimeSlotCreator.Create(1, "Cool", 50, DateTime.Now.AddHours(4), DateTime.Now.AddHours(5)));
            _context.Add(TimeSlotCreator.Create(1, "Cool", 50, DateTime.Now.AddHours(5), DateTime.Now.AddHours(6)));

        }
        private void AddBookings()
        {
            if (_context.Bookings.Any())
            {
                return;
            }

            _context.Add(BookingCreator.Create("test@test.com", 1, 1));
            _context.Add(BookingCreator.Create("test@test.com", 1, 2));
            _context.Add(BookingCreator.Create("test@test.com", 1, 3));
        }
        private void AddEmployees()
        {
            if (_context.Employees.Any())
            {
                return;
            }

            _context.Add(EmployeeCreator.Create(DateTime.Now, 1, "test@test.com", "cool@cool.com"));
            _context.Add(EmployeeCreator.Create(DateTime.Now, 2, "h@h.com", "shop@shop.com"));
        }

        private void AddBusinessOwners()
        {
            if (_context.BusinessOwners.Any())
            {
                return;
            }

            _context.Add(BusinessOwnerCreator.Create(1, "test@test.com"));
        }
    }
}
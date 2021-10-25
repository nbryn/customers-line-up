using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

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

        public async Task InitializeSeed()
        {
            var userIds = AddUsers();
            var businesses = AddBusinesses();
            var businessTimeSlots = AddTimeSlots(businesses);
            AddBookings(businessTimeSlots, businesses.Select(b => b.Id).ToList(), userIds);
            AddEmployees(businesses.Select(b => b.Id).ToList(), userIds);

            await _context.SaveChangesAsync();
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
                                            new Address("Farum Hovedgade 20", "3520", "Farum"), new Coords(55.8200342, 12.3591325));
            _context.Add(user4);
            ids.Add(user4.Id);

            var user5 = UserCreator.Create(new UserData("Niels", "niels@google.com", BC.HashPassword("1234")),
                                            new Address("Kongevejen 466", "2840", "Holte"), new Coords(55.825272, 12.463181));
            _context.Add(user5);
            ids.Add(user5.Id);

            var user6 = UserCreator.Create(new UserData("Kasper", "kasper@yahoo.com", BC.HashPassword("1234")),
                                            new Address("Holte Midtpunkt 30", "2840", "Holte"), new Coords(55.81111, 12.471199));
            _context.Add(user6);
            ids.Add(user6.Id);



            return ids;
        }

        private IList<Business> AddBusinesses()
        {
            var businesses = new List<Business>();
            if (_context.Businesses.Any())
            {
                return businesses;
            }

            var business = BusinessCreator.Create("test@test.com", new BusinessData("Super Brugsen", 50, 30), new TimeSpan("10.00", "22.00"),
                                                 new Address("Ryttergårdsvej 10", "3520", "Farum"), new Coords(55.8137419, 12.3935222), BusinessType.Supermarket);
            _context.Add(business);
            businesses.Add(business);

            var business2 = BusinessCreator.Create("test@test.com", new BusinessData("Farum Museum", 40, 60), new TimeSpan("10.00", "16.00"),
                                                  new Address("Farum Hovedgade 100", "3520", "Farum"), new Coords(55.809127, 12.3544073), BusinessType.Museum);
            _context.Add(business2);
            businesses.Add(business2);


            var business3 = BusinessCreator.Create("test@test.com", new BusinessData("Kvick Kiosk", 30, 10), new TimeSpan("08.30", "23.00"),
                                                  new Address("Vermlandsgade 30", "2300", "København S"), new Coords(55.668442, 12.5988833), BusinessType.Kiosk);
            _context.Add(business3);
            businesses.Add(business3);

            var business4 = BusinessCreator.Create("h@h.com", new BusinessData("HairCut", 2, 40), new TimeSpan("9.00", "18.00"),
                                                 new Address("Amagerbrogade 32", "2840", "Holte"), new Coords(55.8191785, 12.4682046), BusinessType.Hairdresser);
            _context.Add(business4);
            businesses.Add(business4);

            var business5 = BusinessCreator.Create("h@h.com", new BusinessData("Odense Bibliotek", 75, 80), new TimeSpan("08.00", "20.00"),
                                                  new Address("Østre Stationsvej 15", "5000", "Odense"), new Coords(55.40116, 10.388635), BusinessType.Library);
            _context.Add(business5);
            businesses.Add(business5);

            var business6 = BusinessCreator.Create("h@h.com", new BusinessData("Foto", 8, 15), new TimeSpan("10.30", "19.00"),
                                                  new Address("Nord Vej 92", "2605", "Brøndby"), new Coords(55.6678010, 12.4282140), BusinessType.Other);
            _context.Add(business6);
            businesses.Add(business6);

            var business7 = BusinessCreator.Create("h@h.com", new BusinessData("Netto", 40, 20), new TimeSpan("09.00", "14.00"),
                                                 new Address("Lundebjerg 1", "2740", "Skovlunde"), new Coords(55.720478, 12.4016771), BusinessType.Supermarket);
            _context.Add(business7);
            businesses.Add(business7);

            var business8 = BusinessCreator.Create("h@h.com", new BusinessData("Grab'n'Go", 5, 5), new TimeSpan("08.30", "15.30"),
                                                  new Address("Ellemosevej 5", "8370", "Hadsten"), new Coords(56.3306771, 10.0554608), BusinessType.Kiosk);
            _context.Add(business8);
            businesses.Add(business8);


            var owner = new BusinessOwner("test@test.com");
            var owner2 = new BusinessOwner("h@h.com");
            owner.UpdatedAt = DateTime.Now;
            owner2.UpdatedAt = DateTime.Now;
            _context.Add(owner);
            _context.Add(owner2);

            return businesses;
        }

        private Dictionary<string, List<string>> AddTimeSlots(IList<Business> businesses)
        {
            var businessTimeSlots = new Dictionary<string, List<string>>();
            if (_context.TimeSlots.Any())
            {
                return businessTimeSlots;
            }

            foreach (var business in businesses)
            {
                var timeSlots = TimeSlot.GenerateTimeSlots(business, (DateTime.Today.AddDays(1)));
                businessTimeSlots.Add(business.Id, timeSlots.Select(t => t.Id).ToList());

                _context.AddRange(timeSlots);
            }

            return businessTimeSlots;
        }

        private void AddBookings(Dictionary<string, List<string>> businessTimeSlots, IList<string> businessIds, IList<string> userIds)
        {
            if (_context.Bookings.Any())
            {
                return;
            }

            _context.Add(BookingCreator.Create(userIds[0], businessIds[0], businessTimeSlots[businessIds[0]].First()));
            _context.Add(BookingCreator.Create(userIds[0], businessIds[1], businessTimeSlots[businessIds[1]][3]));
            _context.Add(BookingCreator.Create(userIds[0], businessIds[2], businessTimeSlots[businessIds[2]][5]));
            _context.Add(BookingCreator.Create(userIds[0], businessIds[3], businessTimeSlots[businessIds[3]][10]));
            _context.Add(BookingCreator.Create(userIds[0], businessIds[4], businessTimeSlots[businessIds[4]][7]));
            _context.Add(BookingCreator.Create(userIds[0], businessIds[5], businessTimeSlots[businessIds[5]][8]));

            foreach (var businessId in businessIds)
            {
                for (int i = 1, timeSlotCounter = 0; i < 6; i++, timeSlotCounter++)
                {
                    _context.Add(BookingCreator.Create(userIds[i], businessId, businessTimeSlots[businessId][i]));
                }
            }
        }

        private void AddEmployees(IList<string> businessIds, IList<string> userIds)
        {
            if (_context.Employees.Any())
            {
                return;
            }

            _context.Add(EmployeeCreator.Create(businessIds[0], userIds[0], "info@brugsen.dk"));
            _context.Add(EmployeeCreator.Create(businessIds[0], userIds[1], "hej@brugsen.dk"));
            _context.Add(EmployeeCreator.Create(businessIds[1], userIds[2], "info@farum.dk"));
            _context.Add(EmployeeCreator.Create(businessIds[1], userIds[3], "hej@farum.dk"));
            _context.Add(EmployeeCreator.Create(businessIds[2], userIds[4], "info@kiosk.dk"));
            _context.Add(EmployeeCreator.Create(businessIds[2], userIds[5], "hej@kiosk.dk"));
        }
    }
}
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using CLup.Application.Shared.Interfaces;
using CLup.Domain.Bookings;
using CLup.Domain.Businesses;
using CLup.Domain.Businesses.Employees;
using CLup.Domain.Users;
using Microsoft.EntityFrameworkCore;

namespace CLup.Infrastructure.Persistence
{
    public class ReadOnlyDbContext : IReadOnlyDbContext
    {
        private readonly ICLupDbContext _clupContext;

        public ReadOnlyDbContext(ICLupDbContext clupContext)
        {
            _clupContext = clupContext;
        }

        public async Task<IList<Business>> FetchAllBusinesses()
            => await _clupContext.Businesses
                    .Include(business => business.Bookings)
                        .ThenInclude(booking => booking.TimeSlot)
                        .ThenInclude(timeSlot => timeSlot.Business)
                    .Include(business => business.Bookings)
                        .ThenInclude(booking => booking.User)
                    .AsSplitQuery()
                    .ToListAsync();

        public async Task<User> FetchUserAggregate(string userEmail)
            => await _clupContext.Users
                    .Include(user => user.SentMessages)
                    .Include(user => user.ReceivedMessages)
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
                    .FirstOrDefaultAsync(user => user.UserData.Email == userEmail);

        public IQueryable<Booking> Bookings => _clupContext.Bookings.AsNoTracking().AsQueryable();

        public IQueryable<Business> Businesses => _clupContext.Businesses.AsNoTracking().AsQueryable();

        public IQueryable<Employee> Employees => _clupContext.Employees.AsNoTracking().AsQueryable();

        public IQueryable<User> Users => _clupContext.Users.AsNoTracking().AsQueryable();
    }
}
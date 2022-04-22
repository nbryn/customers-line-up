using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using CLup.Application.Shared;
using CLup.Application.Shared.Interfaces;
using CLup.Application.Users;
using CLup.Domain.Businesses;
using CLup.Domain.Users;
using Microsoft.EntityFrameworkCore;

namespace CLup.Infrastructure.Persistence
{
    public class ReadOnlyDbContext : IReadOnlyDbContext
    {
        private readonly ICLupDbContext _clupContext;
        private readonly IMapper _mapper;

        public ReadOnlyDbContext(ICLupDbContext clupContext, IMapper mapper)
        {
            _clupContext = clupContext;
            _mapper = mapper;
        }

        public async Task<IList<Business>> FetchAllBusinesses()
            => await _clupContext.Businesses
                .Include(business => business.Bookings)
                .ThenInclude(booking => booking.TimeSlot)
                .ThenInclude(timeSlot => timeSlot.Business)
                .Include(business => business.Bookings)
                .ThenInclude(booking => booking.User)
                .AsSplitQuery()
                .AsNoTracking()
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
                .AsNoTracking()
                .FirstOrDefaultAsync(user => user.UserData.Email == userEmail);

        public async Task<Result<UsersNotEmployedByBusiness>> FetchUsersNotEmployedByBusiness(string businessId)
        {
            var business = _clupContext.Businesses.FirstOrDefaultAsync(business => business.Id == businessId);

            if (business == null)
            {
                return Result.NotFound<UsersNotEmployedByBusiness>();
            }

            var employeeIds = await _clupContext.Employees
                .Where(employee => employee.BusinessId == businessId)
                .Select(employee => employee.UserId)
                .ToListAsync();

            var users = await _clupContext.Users
                .Where(user => !employeeIds.Contains(user.Id))
                .ToListAsync();

            return Result.Ok(new UsersNotEmployedByBusiness()
                { BusinessId = businessId, Users = _mapper.Map<IList<UserDto>>(users) });
        }
        
        public IQueryable<User> Users => _clupContext.Users.AsNoTracking().AsQueryable();
    }
}
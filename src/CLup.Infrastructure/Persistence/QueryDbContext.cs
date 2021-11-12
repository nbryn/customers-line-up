using System.Linq;
using CLup.Application.Shared.Interfaces;
using CLup.Domain.Booking;
using CLup.Domain.Business;
using CLup.Domain.Business.Employee;
using CLup.Domain.Business.TimeSlot;
using CLup.Domain.User;
using Microsoft.EntityFrameworkCore;

namespace CLup.Infrastructure.Persistence
{
    public class QueryDbContext : IQueryDbContext
    {
        private readonly ICLupDbContext _clupDbContext;

        public QueryDbContext(ICLupDbContext clupDbContext)
        {
            _clupDbContext = clupDbContext;
        }

        public IQueryable<Booking> Bookings => _clupDbContext.Bookings.AsNoTracking().AsQueryable();
        
        public IQueryable<BusinessOwner> BusinessOwners  => _clupDbContext.BusinessOwners.AsNoTracking().AsQueryable();
        
        public IQueryable<Business> Businesses => _clupDbContext.Businesses.AsNoTracking().AsQueryable();
        
        public IQueryable<Employee> Employees => _clupDbContext.Employees.AsNoTracking().AsQueryable();
        
        public IQueryable<TimeSlot> TimeSlots => _clupDbContext.TimeSlots.AsNoTracking().AsQueryable();
        
        public IQueryable<User> Users => _clupDbContext.Users.AsNoTracking().AsQueryable();
        
        public IQueryable<BusinessMessage> BusinessMessages => _clupDbContext.BusinessMessages.AsNoTracking().AsQueryable();
        
        public IQueryable<UserMessage> UserMessages => _clupDbContext.UserMessages.AsNoTracking().AsQueryable();
    }
}
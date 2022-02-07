using System.Linq;
using CLup.Application.Shared.Interfaces;
using CLup.Domain.Bookings;
using CLup.Domain.Businesses;
using CLup.Domain.Businesses.Employees;
using CLup.Domain.Businesses.TimeSlots;
using CLup.Domain.Messages;
using CLup.Domain.Users;
using Microsoft.EntityFrameworkCore;

namespace CLup.Infrastructure.Persistence
{
    public class ReadOnlyDbContext : IReadOnlyDbContext
    {
        private readonly ICLupDbContext _clupDbContext;

        public ReadOnlyDbContext(ICLupDbContext clupDbContext)
        {
            _clupDbContext = clupDbContext;
        }

        public IQueryable<Booking> Bookings => _clupDbContext.Bookings.AsNoTracking().AsQueryable();
        
        public IQueryable<Business> Businesses => _clupDbContext.Businesses.AsNoTracking().AsQueryable();
        
        public IQueryable<Employee> Employees => _clupDbContext.Employees.AsNoTracking().AsQueryable();
        
        public IQueryable<TimeSlot> TimeSlots => _clupDbContext.TimeSlots.AsNoTracking().AsQueryable();
        
        public IQueryable<User> Users => _clupDbContext.Users.AsNoTracking().AsQueryable();
        
        public IQueryable<Message> Messages => _clupDbContext.Messages.AsNoTracking().AsQueryable();
    }
}
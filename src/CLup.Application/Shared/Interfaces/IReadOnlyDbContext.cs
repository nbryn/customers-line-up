using System.Linq;
using CLup.Domain.Bookings;
using CLup.Domain.Businesses;
using CLup.Domain.Businesses.Employees;
using CLup.Domain.Businesses.TimeSlots;
using CLup.Domain.Messages;
using CLup.Domain.Users;

namespace CLup.Application.Shared.Interfaces
{
    public interface IReadOnlyDbContext
    {
        public IQueryable<Booking> Bookings { get; }
        
        public IQueryable<BusinessOwner> BusinessOwners { get; }
        
        public IQueryable<Business> Businesses { get; }
        
        public IQueryable<Employee> Employees { get; }
        
        public IQueryable<TimeSlot> TimeSlots { get; }
        
        public IQueryable<User> Users { get; }
        
        public IQueryable<Message> Messages { get; }
    }
}
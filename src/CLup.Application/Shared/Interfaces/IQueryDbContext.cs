using System.Linq;
using CLup.Domain.Booking;
using CLup.Domain.Business;
using CLup.Domain.Business.Employee;
using CLup.Domain.Business.TimeSlot;
using CLup.Domain.User;

namespace CLup.Application.Shared.Interfaces
{
    public interface IQueryDbContext
    {
        public IQueryable<Booking> Bookings { get; }
        
        public IQueryable<BusinessOwner> BusinessOwners { get; }
        
        public IQueryable<Business> Businesses { get; }
        
        public IQueryable<Employee> Employees { get; }
        
        public IQueryable<TimeSlot> TimeSlots { get; }
        
        public IQueryable<User> Users { get; }
        
        public IQueryable<BusinessMessage> BusinessMessages { get; }
        
        public IQueryable<UserMessage> UserMessages { get; }
    }
}
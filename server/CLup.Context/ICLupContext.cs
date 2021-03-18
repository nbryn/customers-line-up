using Microsoft.EntityFrameworkCore;
using System.Threading;
using System.Threading.Tasks;

using CLup.Bookings;
using CLup.Businesses;
using CLup.Employees;
using CLup.TimeSlots;
using CLup.Users;


namespace CLup.Context
{
    public interface ICLupContext
    {
        DbSet<Booking> Bookings { get; set; }
        DbSet<BusinessOwner> BusinessOwners { get; set; }
        DbSet<Business> Businesses { get; set; }
        DbSet<Employee> Employees { get; set; }
        DbSet<TimeSlot> TimeSlots { get; set; }
        DbSet<User> Users { get; set; }
        Task<int> SaveChangesAsync(CancellationToken cancellationToken = default);
    }
}
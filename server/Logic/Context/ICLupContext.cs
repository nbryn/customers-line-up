using System.Threading;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;

using Logic.Bookings;
using Logic.Businesses;
using Logic.BusinessOwners;
using Logic.Employees;
using Logic.TimeSlots;
using Logic.Users;


namespace Logic.Context
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
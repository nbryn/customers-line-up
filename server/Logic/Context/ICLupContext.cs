using System.Threading;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;

using Logic.Businesses;
using Logic.BusinessOwners;
using Logic.TimeSlots;
using Logic.Users;
using Logic.Bookings;

namespace Logic.Context
{
    public interface ICLupContext
    {
        DbSet<User> Users { get; set; }
        DbSet<BusinessOwner> BusinessOwners { get; set; }
        DbSet<Business> Businesses { get; set; }

        DbSet<TimeSlot> TimeSlots { get; set; }

        DbSet<Booking> Bookings { get; set; }

        Task<int> SaveChangesAsync(CancellationToken cancellationToken = default);
    }
}
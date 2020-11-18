using System.Threading;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;

using Logic.Businesses;
using Logic.BusinessOwners;
using Logic.BusinessQueues;
using Logic.Users;

namespace Logic.Context
{
    public interface ICLupContext
    {
        DbSet<User> Users { get; set; }
        DbSet<BusinessOwner> BusinessOwners { get; set; }
        DbSet<Business> Businesses { get; set; }

        DbSet<BusinessQueue> BusinessQueues { get; set; }

        Task<int> SaveChangesAsync(CancellationToken cancellationToken = default);
    }
}
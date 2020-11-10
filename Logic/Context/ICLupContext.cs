using System.Threading;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;

using Logic.Businesses;
using Logic.BusinessOwners;
using Logic.Users;

namespace Logic.Context
{
    public interface ICLupContext
    {
        DbSet<User> Users { get; }
        DbSet<BusinessOwner> BusinessOwners { get; }
        DbSet<Business> Businesses { get; }

        Task<int> SaveChangesAsync(CancellationToken cancellationToken = default);
    }
}
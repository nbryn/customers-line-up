using System.Threading;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;

using CLup.User;

namespace CLup.DB
{
    public interface ICLupContext
    {
        DbSet<User> Users { get; }
    }
}
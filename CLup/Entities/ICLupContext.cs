using System.Threading;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;

namespace CLup.Entities
{
    public interface ICLupContext
    {
        DbSet<User> Users { get; }
    }
}
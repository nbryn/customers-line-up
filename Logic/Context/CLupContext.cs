using Microsoft.EntityFrameworkCore;

using Logic.Users;

namespace Logic.Context
{
    public class CLupContext : DbContext, ICLupContext
    {
        public CLupContext(DbContextOptions<CLupContext> options)
            : base(options)
        {
        }

        public DbSet<User> Users { get; set; }
    }
}
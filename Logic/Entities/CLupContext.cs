using Microsoft.EntityFrameworkCore;

namespace Logic.Entities
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
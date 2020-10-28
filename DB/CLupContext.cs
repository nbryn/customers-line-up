using Microsoft.EntityFrameworkCore;

namespace CLup.DB
{
    public class CLupContext : DbContext
    {
        public CLupContext(DbContextOptions<CLupContext> options)
            : base(options)
        {
        }

        public DbSet<User> users { get; set; }
    }
}
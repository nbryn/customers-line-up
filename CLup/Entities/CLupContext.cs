using Microsoft.EntityFrameworkCore;

namespace CLup.Entities
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
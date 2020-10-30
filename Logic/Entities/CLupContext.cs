using Microsoft.EntityFrameworkCore;

namespace Logic.Entities
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
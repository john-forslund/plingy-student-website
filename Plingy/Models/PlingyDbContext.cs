using Microsoft.EntityFrameworkCore;

namespace Plingy.Models
{
    public class PlingyDbContext : DbContext
    {
        public PlingyDbContext (DbContextOptions<PlingyDbContext> options)
            : base(options)
        {
        }

        public DbSet<Student> Student { get; set; }
        public DbSet<Allergies> Allergies { get; set; }
    }
}

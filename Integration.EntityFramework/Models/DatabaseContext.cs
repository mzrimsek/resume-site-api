using Microsoft.EntityFrameworkCore;

namespace Integration.EntityFramework.Models
{
    public class DatabaseContext : DbContext
    {
        public DbSet<Job> Jobs { get; set; }
        
        public DatabaseContext(DbContextOptions<DatabaseContext> options) : base(options) {}

        protected override void OnModelCreating(ModelBuilder builder) 
        {
            base.OnModelCreating(builder);
        }
    }
}
using Microsoft.EntityFrameworkCore;

namespace Integration.EntityFramework.Models
{
    public class DatabaseContext : DbContext
    {
        public DbSet<Work> Work { get; set; }
        
        public DatabaseContext(DbContextOptions<DatabaseContext> options) : base(options) {}

        protected override void OnModelCreating(ModelBuilder builder) 
        {
            base.OnModelCreating(builder);
        }
    }
}
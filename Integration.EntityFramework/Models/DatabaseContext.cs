using Microsoft.EntityFrameworkCore;

namespace Integration.EntityFramework.Models
{
    public class DatabaseContext : DbContext
    {
        public DbSet<JobDatabaseModel> Jobs { get; set; }
        public DbSet<JobProjectDatabaseModel> JobProjects { get; set; }
        public DbSet<SchoolDatabaseModel> Schools { get; set; }
        public DbSet<LanguageDatabaseModel> Languages { get; set; }

        public DatabaseContext(DbContextOptions<DatabaseContext> options) : base(options) { }

        protected override void OnModelCreating(ModelBuilder builder)
        {
            base.OnModelCreating(builder);
        }
    }
}
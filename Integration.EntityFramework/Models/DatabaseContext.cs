using Microsoft.EntityFrameworkCore;

namespace Integration.EntityFramework.Models
{
    public class DatabaseContext : DbContext
    {
        public DbSet<JobDatabaseModel> Jobs { get; set; }
        public DbSet<JobProjectDatabaseModel> JobProjects { get; set; }
        public DbSet<SchoolDatabaseModel> Schools { get; set; }
        public DbSet<LanguageDatabaseModel> Languages { get; set; }
        public DbSet<SkillDatabaseModel> Skills { get; set; }
        public DbSet<SocialMediaLinkDatabaseModel> SocialMediaLinks { get; set; }
        public DbSet<ProjectDatabaseModel> Projects { get; set; }

        public DatabaseContext(DbContextOptions options) : base(options) { }

        protected override void OnModelCreating(ModelBuilder builder)
        {
            base.OnModelCreating(builder);
        }
    }
}
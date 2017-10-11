using Core.Interfaces.RepositoryInterfaces;
using Integration.EntityFramework.Repositories;
using Microsoft.Extensions.DependencyInjection;

namespace Web.Configuration
{
    public static class DependencyInjectionConfiguration
    {
        public static void Configure(IServiceCollection services)
        {
            services.AddScoped<IJobRepository, JobRepository>();
            services.AddScoped<IJobProjectRepository, JobProjectRepository>();
            services.AddScoped<ISchoolRepository, SchoolRepository>();
            services.AddScoped<ILanguageRepository, LanguageRepository>();
            services.AddScoped<ISkillRepository, SkillRepository>();
            services.AddScoped<ISocialMediaLinkRepository, SocialMediaLinkRepository>();
            services.AddScoped<IProjectRepository, ProjectRepository>();
        }
    }
}
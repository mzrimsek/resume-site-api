using Microsoft.Extensions.DependencyInjection;
using Core.Interfaces.RepositoryInterfaces;
using Integration.EntityFramework.Repositories;

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
        }
    }
}
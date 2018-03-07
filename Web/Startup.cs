using System;
using AutoMapper;
using Integration.EntityFramework.Models;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Web.Configuration;

namespace Web
{
    public class Startup
    {
        public Startup(IHostingEnvironment env)
        {
            var builder = new ConfigurationBuilder()
                .SetBasePath(env.ContentRootPath)
                .AddJsonFile($"appsettings.{env.EnvironmentName}.json", optional: true);
            Configuration = builder.Build();
        }

        public IConfiguration Configuration { get; }

        public void ConfigureServices(IServiceCollection services)
        {
            var connectionString = GetConnectionStringFromEnvironment();
            services.AddDbContext<DatabaseContext>(options => options.UseNpgsql(connectionString));
            services.AddAutoMapper();
            services.AddMvc();
            services.ConfigureRepositories();
        }

        public void Configure(IApplicationBuilder app, IHostingEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }

            app.UseMvc();
        }

        private string GetConnectionStringFromEnvironment()
        {
            var databaseUser = Environment.GetEnvironmentVariable("DATABASE_USER");
            var databasePass = Environment.GetEnvironmentVariable("DATABASE_PASS");
            var databaseName = Environment.GetEnvironmentVariable("DATABASE_NAME");
            var databaseHost = Environment.GetEnvironmentVariable("DATABASE_HOST");
            var databasePort = Environment.GetEnvironmentVariable("DATABASE_PORT");

            return $"User ID={databaseUser};Password={databasePass};Host={databaseHost};Port={databasePort};Database={databaseName};Pooling=true;";
        }
    }
}

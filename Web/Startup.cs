﻿using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Core.Interfaces;
using Integration.EntityFramework.Models;
using Integration.EntityFramework.Repositories;

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
            string connectionString = GetConnectionStringFromEnvironment();
            services.AddDbContext<DatabaseContext>(options => options.UseNpgsql(connectionString));
            services.AddMvc();

            services.AddScoped<IJobRepository, JobRepository>();
            services.AddScoped<IJobProjectRepository, JobProjectRepository>();
            services.AddScoped<ISchoolRepository, SchoolRepository>();
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

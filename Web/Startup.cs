using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Integration.EntityFramework.Models;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;

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
      var databaseUser = Environment.GetEnvironmentVariable("RESUME_DATABASE_USER");
      var databasePass = Environment.GetEnvironmentVariable("RESUME_DATABASE_PASS");
      var databaseName = Environment.GetEnvironmentVariable("RESUME_DATABASE_NAME");
      var databaseHost = Environment.GetEnvironmentVariable("RESUME_DATABASE_HOST");
      var databasePort = Environment.GetEnvironmentVariable("RESUME_DATABASE_PORT");

      return $"User ID={databaseUser};Password={databasePass};Host={databaseHost};Port={databasePort};Database={databaseName};Pooling=true;";
    }
  }
}

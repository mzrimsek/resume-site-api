using Microsoft.Extensions.DependencyInjection;
using Swashbuckle.AspNetCore.Swagger;

namespace Web.Configuration
{
    public static class SwaggerConfiguration
    {
        public static void Configure(IServiceCollection services)
        {
            services.AddSwaggerGen(c =>
            {
                c.SwaggerDoc("v1", new Info { 
                    Title = "Resume Site API", 
                    Version = "v1",
                    Contact = new Contact {
                        Name = "Mike Zrimsek",
                        Email = "mikezrimsek@gmail.com"
                    },
                    License = new License
                    {
                        Name = "MIT",
                        Url = "https://opensource.org/licenses/MIT"
                    }
                });
            });
        }
    }
}
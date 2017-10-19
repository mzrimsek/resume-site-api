using Microsoft.Extensions.DependencyInjection;
using Swashbuckle.AspNetCore.Swagger;

namespace Web.Configuration
{
    public static class SwaggerConfiguration
    {
        public static string TITLE = "Resume Site API";
        public static string VERSION = "v1";
        
        public static void Configure(IServiceCollection services)
        {
            services.AddSwaggerGen(c =>
            {
                c.SwaggerDoc(VERSION, new Info { 
                    Title = TITLE, 
                    Version = VERSION,
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
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;

namespace Common.Api
{
    public abstract class ApiStartup
    {
        protected IHostingEnvironment Environment { get; }

        protected IConfiguration Configuration { get; set; }

        protected ApiStartup(IHostingEnvironment environment)
        {
            IConfigurationBuilder builder = new ConfigurationBuilder()
                .SetBasePath(environment.ContentRootPath)
                .AddJsonFile("appSettings.json", false, true)
                .AddJsonFile("secrets.json", false, true)
                .AddJsonFile("hostSettings.json", false, true)
                .AddEnvironmentVariables();

            Configuration = builder.Build();
            Environment = environment;
        }
    }
}
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using PaladinRogue.Libray.Configuration.Secrets.Kms;

namespace PaladinRogue.Libray.Core.Api
{
    public abstract class ApiStartup
    {
        protected IHostingEnvironment Environment { get; }

        protected IConfiguration Configuration { get; set; }

        protected ApiStartup(IHostingEnvironment environment)
        {
            IConfigurationBuilder builder = new ConfigurationBuilder()
                .SetBasePath(environment.ContentRootPath)
                .AddJsonFile("appSettings.json", false, false)
                .AddJsonFile(new EncryptedFileProvider(), "appSecrets.json.encrypted", true, false)
                .AddJsonFile("hostSettings.json", false, false)
                .AddEnvironmentVariables();

            Configuration = builder.Build();
            Environment = environment;
        }
    }
}
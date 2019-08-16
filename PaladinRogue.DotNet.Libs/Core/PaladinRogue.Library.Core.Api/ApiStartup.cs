using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using PaladinRogue.Library.Configuration.Secrets.Kms;

namespace PaladinRogue.Library.Core.Api
{
    public abstract class ApiStartup
    {
        protected IHostingEnvironment Environment { get; }

        protected IConfiguration Configuration { get; }

        protected ApiStartup(IHostingEnvironment environment)
        {
            IConfigurationBuilder builder = new ConfigurationBuilder()
                .SetBasePath(environment.ContentRootPath)
                .AddEnvironmentVariables()
                .AddJsonFile("appSettings.json", false, false)
                .AddJsonFile(new EncryptedFileProvider(environment.ContentRootPath), "appSecrets.json.encrypted", true, false)
                .AddJsonFile("hostSettings.json", false, false);

            Configuration = builder.Build();
            Environment = environment;
        }
    }
}
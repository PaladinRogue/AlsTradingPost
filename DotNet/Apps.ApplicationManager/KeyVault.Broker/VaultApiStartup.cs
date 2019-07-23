using Common.Api;
using KeyVault.Broker.Setup.Settings;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;

namespace KeyVault.Broker
{
    public class VaultApiStartup : ApiStartup
    {
        public VaultApiStartup(IHostingEnvironment environment) : base(environment)
        {
            IConfigurationRoot configurationRoot = new ConfigurationBuilder()
                .SetBasePath(environment.ContentRootPath)
                .AddJsonFile("secrets.json", false, true)
                .Build();

            VaultSettings vaultSettings = new VaultSettings();
            configurationRoot.GetSection(nameof(VaultSettings)).Bind(vaultSettings);

            Configuration = new ConfigurationBuilder()
                .AddConfiguration(Configuration)
                .AddJsonFile(vaultSettings.Location, false, true)
                .Build();
        }
    }
}
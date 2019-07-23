using System.IO;
using System.Threading.Tasks;
using Common.Setup.Infrastructure.Persistence;
using KeyVault.Setup.Infrastructure.DataKeys;
using Microsoft.AspNetCore;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;

namespace KeyVault.Api
{
    public class Program
    {
        public static async Task Main(string[] args)
        {
            IWebHost builder = CreateWebHostBuilder(args)
                .Build()
                .ApplyMigrations();

            await builder.CreateSharedDataKeysAsync();

            builder.Run();
        }

        public static IWebHostBuilder CreateWebHostBuilder(string[] args)
        {
            IConfigurationBuilder builder = new ConfigurationBuilder()
                .SetBasePath(Directory.GetCurrentDirectory())
                .AddJsonFile("hostSettings.json", false)
                .AddEnvironmentVariables();

            return WebHost.CreateDefaultBuilder(args)
                .UseStartup<Startup>()
                .UseConfiguration(builder.Build());
        }
    }
}
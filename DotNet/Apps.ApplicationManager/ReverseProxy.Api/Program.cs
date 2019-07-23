using System.IO;
using System.Threading.Tasks;
using Common.Setup.Infrastructure.Messaging;
using Common.Setup.Infrastructure.Persistence;
using KeyVault.Broker;
using Microsoft.AspNetCore;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using ReverseProxy.Domain;

namespace ReversProxy.Api
{
    public class Program
    {
        public static async Task Main(string[] args)
        {
            IWebHost builder = CreateWebHostBuilder(args)
                .Build()
                .ApplyMigrations()
                .InitialiseMessaging();

            await builder.CreateDataKeysAsync<DataKeys>();

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
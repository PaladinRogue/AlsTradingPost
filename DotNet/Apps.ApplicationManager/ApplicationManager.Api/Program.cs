using System.IO;
using Common.Api.ApplicationRegistration;
using Common.Setup.Infrastructure.Messaging;
using Common.Setup.Infrastructure.Persistence;
using Microsoft.AspNetCore;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;

namespace ApplicationManager.Api
{
    public class Program
    {
        public static void Main(string[] args)
        {
            CreateWebHostBuilder(args)
                .Build()
                .ApplyMigrations()
                .InitialiseMessaging()
                .RegisterApplication()
                .Run();
        }

        public static IWebHostBuilder CreateWebHostBuilder(string[] args)
        {
            IConfigurationBuilder builder = new ConfigurationBuilder()
                .SetBasePath(Directory.GetCurrentDirectory())
                .AddJsonFile("hostsettings.json", false)
                .AddEnvironmentVariables();

            return WebHost.CreateDefaultBuilder(args)
                .UseStartup<Startup>()
                .UseConfiguration(builder.Build());
        }
    }
}

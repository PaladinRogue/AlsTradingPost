using System.IO;
using System.Threading.Tasks;
using Common.Setup.Infrastructure.Startup;
using Microsoft.AspNetCore;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;

namespace ApplicationManager.Api
{
    public class Program
    {
        public static Task Main(string[] args)
        {
            return CreateWebHostBuilder(args)
                .Build()
                .RunWithStartupTasksAsync();
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
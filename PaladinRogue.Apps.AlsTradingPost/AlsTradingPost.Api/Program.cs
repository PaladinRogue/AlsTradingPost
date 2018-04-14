using AlsTradingPost.Setup.Infrastructure.DbInitializer;
using Common.Resources.Authentication;
using Common.Resources.Extensions;
using Common.Setup.Extentions;
using Microsoft.AspNetCore;
using Microsoft.AspNetCore.Hosting;

namespace AlsTradingPost.Api
{
    public class Program
    {
        public static void Main(string[] args)
        {
            BuildWebHost(args)
                .RegisterApplication("AlsTradingPost", AuthenticationProtocol.Facebook)
                .ApplyMigrations()
                .SeedData()
                .Run();
        }

        public static IWebHost BuildWebHost(string[] args) =>
            WebHost.CreateDefaultBuilder(args)
                .UseStartup<Startup>()
                .UseIISIntegration()
				.Build();
    }
}

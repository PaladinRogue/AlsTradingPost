using AlsTradingPost.Setup.Infrastructure.DbInitializer;
using Common.Api.ApplicationRegistration;
using Common.Resources.Authentication;
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
                .UseUrls("http://localhost:1001")
                .UseStartup<Startup>()
                .UseKestrel()
                .UseIISIntegration()
				.Build();
    }
}

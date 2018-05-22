using AlsTradingPost.Persistence;
using AlsTradingPost.Setup.Infrastructure.DbInitializer;
using Common.Api.ApplicationRegistration;
using Common.Resources.Authentication;
using Microsoft.AspNetCore;
using Microsoft.AspNetCore.Hosting;
using Persistence.EntityFramework.Infrastructure.Extensions;

namespace AlsTradingPost.Api
{
    public class Program
    {
        public static void Main(string[] args)
        {
            CreateWebHostBuilder(args)
                .Build()
                .RegisterApplication("AlsTradingPost", AuthenticationProtocol.Facebook)
                .ApplyMigrations<AlsTradingPostDbContext>()
                .SeedData()
                .Run();
        }

        public static IWebHostBuilder CreateWebHostBuilder(string[] args) =>
            WebHost.CreateDefaultBuilder(args)
                .UseUrls("https://localhost:1001")
                .UseStartup<Startup>()
                .UseKestrel()
                .UseIISIntegration();
    }
}

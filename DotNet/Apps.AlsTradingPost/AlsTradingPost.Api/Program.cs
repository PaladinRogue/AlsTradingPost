using System;
using AlsTradingPost.Persistence;
using AlsTradingPost.Resources;
using AlsTradingPost.Setup.Infrastructure.DbInitializer;
using AlsTradingPost.Setup.Infrastructure.Routing;
using Common.Api.ApplicationRegistration;
using Common.Api.Routing;
using Common.Resources.Authentication;
using Microsoft.AspNetCore;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.DependencyInjection;
using Persistence.EntityFramework.Infrastructure.Extensions;

namespace AlsTradingPost.Api
{
    public class Program
    {
        private const string HostUrl = "https://localhost:1001";

        public static void Main(string[] args)
        {
            IWebHost webHost = CreateWebHostBuilder(args)
                .Build();

            IServiceProvider serviceProvider = webHost.Services;
            IRouteProvider<PersonaFlags> routeProvider = serviceProvider.GetRequiredService<IRouteProvider<PersonaFlags>>();

            string routeTemplate = routeProvider.GetRouteTemplate<object>(RouteDictionary.AuthenticationLoginTemplate, PersonaFlags.None, null);

            webHost.RegisterApplication("AlsTradingPost", HostUrl + routeTemplate, AuthenticationProtocol.Facebook)
                .ApplyMigrations<AlsTradingPostDbContext>()
                .SeedData()
                .Run();
        }

        public static IWebHostBuilder CreateWebHostBuilder(string[] args) =>
            WebHost.CreateDefaultBuilder(args)
                .UseUrls(HostUrl)
                .UseStartup<Startup>()
                .UseKestrel()
                .UseIISIntegration();
    }
}

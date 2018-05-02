using System;
using Authentication.Persistence;
using Microsoft.AspNetCore.Hosting;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;

namespace Authentication.Setup.Infrastructure.DbInitializer
{
    public static class WebHostExtensions
    {
        public static IWebHost ApplyMigrations(this IWebHost host)
        {
            using (IServiceScope scope = host.Services.CreateScope())
            {
                IServiceProvider serviceProvider = scope.ServiceProvider;
                AuthenticationDbContext context = serviceProvider.GetService<AuthenticationDbContext>();

                if (!context.Database.EnsureCreated())
                {
                    context.Database.Migrate();
                }
            }
            return host;
        }
    }
}

using System;
using Microsoft.AspNetCore.Hosting;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;

namespace Common.Setup.Infrastructure.Persistence
{
    public static class WebHostExtensions
    {
        public static IWebHost ApplyMigrations(this IWebHost host)
        {
            using (IServiceScope scope = host.Services.CreateScope())
            {
                IServiceProvider serviceProvider = scope.ServiceProvider;
                DbContext context = serviceProvider.GetService<DbContext>();

                context.Database.Migrate();

                return host;
            }
        }
    }
}

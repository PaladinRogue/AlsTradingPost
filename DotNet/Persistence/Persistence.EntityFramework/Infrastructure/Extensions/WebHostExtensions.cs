using System;
using Microsoft.AspNetCore.Hosting;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;

namespace Persistence.EntityFramework.Infrastructure.Extensions
{
    public static class WebHostExtensions
    {
        public static IWebHost ApplyMigrations<T>(this IWebHost host) where T: DbContext
        {
            using (IServiceScope scope = host.Services.CreateScope())
            {
                IServiceProvider serviceProvider = scope.ServiceProvider;
                T context = serviceProvider.GetService<T>();

                context.Database.Migrate();
            }
            return host;
        }
    }
}

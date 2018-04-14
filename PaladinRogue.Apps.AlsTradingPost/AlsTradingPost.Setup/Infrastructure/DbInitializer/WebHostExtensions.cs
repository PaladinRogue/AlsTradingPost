using System;
using AlsTradingPost.Persistence;
using Common.Resources.Transactions;
using Microsoft.AspNetCore.Hosting;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;

namespace AlsTradingPost.Setup.Infrastructure.DbInitializer
{
    public static class WebHostExtensions
    {
        public static IWebHost ApplyMigrations(this IWebHost host)
        {
            using (IServiceScope scope = host.Services.CreateScope())
            {
                IServiceProvider serviceProvider = scope.ServiceProvider;
                AlsTradingPostDbContext context = serviceProvider.GetService<AlsTradingPostDbContext>();

                if (!context.Database.EnsureCreated())
                {
                    context.Database.Migrate();
                }
            }
            return host;
        }

        public static IWebHost SeedData(this IWebHost host)
        {
            using (IServiceScope scope = host.Services.CreateScope())
            {
                IServiceProvider serviceProvider = scope.ServiceProvider;
                AlsTradingPostDbContext context = serviceProvider.GetService<AlsTradingPostDbContext>();
                ITransactionFactory transactionFactory = serviceProvider.GetService<ITransactionFactory>();

                using (ITransaction transaction = transactionFactory.Create())
                {
                    DbInitializer.Seed(context);

                    transaction.Commit();
                }
            }
            return host;
        }
    }
}

using System;
using AlsTradingPost.Persistence;
using Common.Application.Transactions;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.DependencyInjection;

namespace AlsTradingPost.Setup.Infrastructure.DbInitializer
{
    public static class WebHostExtensions
    {
        public static IWebHost SeedData(this IWebHost host)
        {
            using (IServiceScope scope = host.Services.CreateScope())
            {
                IServiceProvider serviceProvider = scope.ServiceProvider;
                AlsTradingPostDbContext context = serviceProvider.GetService<AlsTradingPostDbContext>();
                ITransactionManager transactionManager = serviceProvider.GetService<ITransactionManager>();

                using (ITransaction transaction = transactionManager.Create())
                {
                    DbInitializer.Seed(context);

                    transaction.Commit();
                }
            }
            return host;
        }
    }
}

using System;
using System.Threading;
using System.Threading.Tasks;
using Common.Setup.Infrastructure.Startup;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;

namespace Common.Setup.Infrastructure.Persistence
{
    public class ApplyMigrationsStartupTask : IStartupTask
    {
        private readonly IServiceProvider _serviceProvider;

        public ApplyMigrationsStartupTask(IServiceProvider serviceProvider)
        {
            _serviceProvider = serviceProvider;
        }

        public async Task ExecuteAsync(CancellationToken cancellationToken = default)
        {
            using (IServiceScope scope = _serviceProvider.CreateScope())
            {
                IServiceProvider serviceProvider = scope.ServiceProvider;
                DbContext context = serviceProvider.GetService<DbContext>();

                await context.Database.MigrateAsync(cancellationToken);
            }
        }
    }
}

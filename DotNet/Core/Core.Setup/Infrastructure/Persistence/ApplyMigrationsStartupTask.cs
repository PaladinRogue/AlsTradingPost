using System;
using System.Threading;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using PaladinRogue.Libray.Core.Setup.Infrastructure.Startup;

namespace PaladinRogue.Libray.Core.Setup.Infrastructure.Persistence
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

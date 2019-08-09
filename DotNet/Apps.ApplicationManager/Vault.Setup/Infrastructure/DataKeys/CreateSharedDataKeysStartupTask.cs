using System;
using System.Threading;
using System.Threading.Tasks;
using Common.Setup.Infrastructure.Startup;
using Vault.Application.SharedDataKeys.Create;
using Microsoft.Extensions.DependencyInjection;

namespace Vault.Setup.Infrastructure.DataKeys
{
    public class CreateSharedDataKeysStartupTask : IStartupTask
    {
        private readonly IServiceProvider _serviceProvider;

        public CreateSharedDataKeysStartupTask(IServiceProvider serviceProvider)
        {
            _serviceProvider = serviceProvider;
        }

        public async Task ExecuteAsync(CancellationToken cancellationToken = default)
        {
            using (IServiceScope scope = _serviceProvider.CreateScope())
            {
                IServiceProvider serviceProvider = scope.ServiceProvider;

                ICreateSharedDataKeysApplicationKernalService createSharedDataKeysApplicationKernalService = serviceProvider.GetRequiredService<ICreateSharedDataKeysApplicationKernalService>();

                await createSharedDataKeysApplicationKernalService.ExecuteAsync();
            }
        }
    }
}
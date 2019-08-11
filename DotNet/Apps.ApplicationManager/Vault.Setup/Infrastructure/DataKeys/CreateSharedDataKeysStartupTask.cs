using System;
using System.Threading;
using System.Threading.Tasks;
using Microsoft.Extensions.DependencyInjection;
using PaladinRogue.Libray.Core.Setup.Infrastructure.Startup;
using PaladinRogue.Vault.Application.SharedDataKeys.Create;

namespace PaladinRogue.Vault.Setup.Infrastructure.DataKeys
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
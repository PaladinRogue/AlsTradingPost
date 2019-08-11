using System;
using System.Threading;
using System.Threading.Tasks;
using Microsoft.Extensions.DependencyInjection;
using PaladinRogue.Libray.Core.Setup.Infrastructure.Startup;
using PaladinRogue.Libray.Vault.Application.DataKeys;

namespace PaladinRogue.Libray.Vault.Setup.Infrastructure.Startup
{
    public class CreateDataKeysStartupTask<T> : IStartupTask
    {
        private readonly IServiceProvider _serviceProvider;

        public CreateDataKeysStartupTask(IServiceProvider serviceProvider)
        {
            _serviceProvider = serviceProvider;
        }

        public async Task ExecuteAsync(CancellationToken cancellationToken = default)
        {
            using (IServiceScope scope = _serviceProvider.CreateScope())
            {
                IServiceProvider serviceProvider = scope.ServiceProvider;

                ICacheDataKeyApplicationKernalService cacheDataKeyApplicationKernalService = serviceProvider.GetService<ICacheDataKeyApplicationKernalService>();

                await cacheDataKeyApplicationKernalService.CreateAndCacheAllAsync<T>();
            }
        }
    }
}
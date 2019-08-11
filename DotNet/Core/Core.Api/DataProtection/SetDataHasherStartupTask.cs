using System;
using System.Threading;
using System.Threading.Tasks;
using Microsoft.Extensions.DependencyInjection;
using PaladinRogue.Libray.Core.Domain.DataProtectors;
using PaladinRogue.Libray.Core.Setup.Infrastructure.Startup;

namespace PaladinRogue.Libray.Core.Api.DataProtection
{
    public class SetDataHasherStartupTask : IStartupTask
    {
        private readonly IServiceProvider _serviceProvider;

        public SetDataHasherStartupTask(IServiceProvider serviceProvider)
        {
            _serviceProvider = serviceProvider;
        }

        public Task ExecuteAsync(CancellationToken cancellationToken = default)
        {
            _serviceProvider.GetRequiredService<IDataHasher>().SetDataHasher();

            return Task.CompletedTask;
        }
    }
}
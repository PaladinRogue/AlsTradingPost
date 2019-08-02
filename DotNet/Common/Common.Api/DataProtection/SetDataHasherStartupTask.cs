using System;
using System.Threading;
using System.Threading.Tasks;
using Common.Domain.DataProtectors;
using Common.Setup.Infrastructure.Startup;
using Microsoft.Extensions.DependencyInjection;

namespace Common.Api.DataProtection
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
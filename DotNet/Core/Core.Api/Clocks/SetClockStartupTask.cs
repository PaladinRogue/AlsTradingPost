using System;
using System.Threading;
using System.Threading.Tasks;
using Microsoft.Extensions.DependencyInjection;
using NodaTime;
using PaladinRogue.Libray.Core.Domain.Clocks;
using PaladinRogue.Libray.Core.Setup.Infrastructure.Startup;

namespace PaladinRogue.Libray.Core.Api.Clocks
{
    public class SetClockStartupTask : IStartupTask
    {
        private readonly IServiceProvider _serviceProvider;

        public SetClockStartupTask(IServiceProvider serviceProvider)
        {
            _serviceProvider = serviceProvider;
        }

        public Task ExecuteAsync(CancellationToken cancellationToken = default)
        {
            _serviceProvider.GetRequiredService<IClock>().SetClock();

            return Task.CompletedTask;
        }
    }
}
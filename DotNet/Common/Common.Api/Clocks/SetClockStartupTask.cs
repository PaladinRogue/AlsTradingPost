using System;
using System.Threading;
using System.Threading.Tasks;
using Common.Domain.Clocks;
using Common.Setup.Infrastructure.Startup;
using Microsoft.Extensions.DependencyInjection;
using NodaTime;

namespace Common.Api.Clocks
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
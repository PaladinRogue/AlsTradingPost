using System;
using System.Threading;
using System.Threading.Tasks;
using Common.Domain.DomainEvents;
using Common.Domain.DomainEvents.Interfaces;
using Common.Setup.Infrastructure.Startup;
using Microsoft.Extensions.DependencyInjection;

namespace Common.Api.DomainEvents
{
    public class SetDomainEventDispatcherStartupTask : IStartupTask
    {
        private readonly IServiceProvider _serviceProvider;

        public SetDomainEventDispatcherStartupTask(IServiceProvider serviceProvider)
        {
            _serviceProvider = serviceProvider;
        }

        public Task ExecuteAsync(CancellationToken cancellationToken = default)
        {
            _serviceProvider.GetRequiredService<IDomainEventDispatcher>().SetDomainEventDispatcher();

            return Task.CompletedTask;
        }
    }
}
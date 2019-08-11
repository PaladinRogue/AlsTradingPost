using System;
using System.Threading;
using System.Threading.Tasks;
using Microsoft.Extensions.DependencyInjection;
using PaladinRogue.Libray.Core.Domain.DomainEvents;
using PaladinRogue.Libray.Core.Domain.DomainEvents.Interfaces;
using PaladinRogue.Libray.Core.Setup.Infrastructure.Startup;

namespace PaladinRogue.Libray.Core.Api.DomainEvents
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
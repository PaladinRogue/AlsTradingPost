using System;
using System.Threading;
using System.Threading.Tasks;
using Microsoft.Extensions.DependencyInjection;
using PaladinRogue.Library.Core.Domain.DomainEvents;
using PaladinRogue.Library.Core.Domain.DomainEvents.Interfaces;
using PaladinRogue.Library.Core.Setup.Infrastructure.Startup;

namespace PaladinRogue.Library.Core.Api.DomainEvents
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
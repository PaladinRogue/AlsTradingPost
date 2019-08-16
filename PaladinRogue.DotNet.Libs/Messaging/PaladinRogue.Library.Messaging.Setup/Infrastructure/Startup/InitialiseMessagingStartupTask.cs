using System;
using System.Threading;
using System.Threading.Tasks;
using Microsoft.Extensions.DependencyInjection;
using PaladinRogue.Library.Core.Setup.Infrastructure.Startup;
using PaladinRogue.Library.Messaging.Setup.Infrastructure.Handlers;

namespace PaladinRogue.Library.Messaging.Setup.Infrastructure.Startup
{
    public class InitialiseMessagingStartupTask : IStartupTask
    {
        private readonly IServiceProvider _serviceProvider;

        public InitialiseMessagingStartupTask(IServiceProvider serviceProvider)
        {
            _serviceProvider = serviceProvider;
        }

        public async Task ExecuteAsync(CancellationToken cancellationToken = default)
        {
            using (IServiceScope scope = _serviceProvider.CreateScope())
            {
                IServiceProvider serviceProvider = scope.ServiceProvider;
                IMessageHandlerFactory messageHandlerFactory = serviceProvider.GetRequiredService<IMessageHandlerFactory>();

                await messageHandlerFactory.InitialiseAsync();
            }
        }
    }
}

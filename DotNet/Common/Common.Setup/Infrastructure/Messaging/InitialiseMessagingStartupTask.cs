using System;
using System.Threading;
using System.Threading.Tasks;
using Common.Messaging.Infrastructure.Subscribers;
using Common.Setup.Infrastructure.Startup;
using Microsoft.Extensions.DependencyInjection;

namespace Common.Setup.Infrastructure.Messaging
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
                IMessageSubscriberFactory messageSubscriberFactory = serviceProvider.GetRequiredService<IMessageSubscriberFactory>();

                await messageSubscriberFactory.InitialiseAsync();
            }
        }
    }
}

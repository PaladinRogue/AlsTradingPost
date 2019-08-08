using System;
using System.Threading;
using System.Threading.Tasks;
using Common.Setup.Infrastructure.Startup;
using Messaging.Setup.Infrastructure.Senders;
using Microsoft.Extensions.DependencyInjection;

namespace Messaging.Setup.Infrastructure.Startup
{
    public class SetMessageSenderStartupTask : IStartupTask
    {
        private readonly IServiceProvider _serviceProvider;

        public SetMessageSenderStartupTask(IServiceProvider serviceProvider)
        {
            _serviceProvider = serviceProvider;
        }

        public Task ExecuteAsync(CancellationToken cancellationToken = default)
        {
            _serviceProvider.GetRequiredService<IMessageSender>().SetMessageSender();

            return Task.CompletedTask;
        }
    }
}
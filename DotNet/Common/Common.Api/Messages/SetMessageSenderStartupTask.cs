using System;
using System.Threading;
using System.Threading.Tasks;
using Common.Messaging.Infrastructure;
using Common.Messaging.Infrastructure.Senders;
using Common.Setup.Infrastructure.Startup;
using Microsoft.Extensions.DependencyInjection;

namespace Common.Api.Messages
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
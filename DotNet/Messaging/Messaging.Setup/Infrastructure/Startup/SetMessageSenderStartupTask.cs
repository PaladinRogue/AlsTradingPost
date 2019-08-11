using System;
using System.Threading;
using System.Threading.Tasks;
using Microsoft.Extensions.DependencyInjection;
using PaladinRogue.Libray.Core.Setup.Infrastructure.Startup;
using PaladinRogue.Libray.Messaging.Common.Messages;
using PaladinRogue.Libray.Messaging.Common.Senders;

namespace PaladinRogue.Libray.Messaging.Setup.Infrastructure.Startup
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
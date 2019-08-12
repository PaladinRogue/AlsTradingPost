using System;
using System.Threading;
using System.Threading.Tasks;
using Microsoft.Extensions.DependencyInjection;
using PaladinRogue.Library.Core.Setup.Infrastructure.Startup;
using PaladinRogue.Library.Messaging.Common.Messages;
using PaladinRogue.Library.Messaging.Common.Senders;

namespace PaladinRogue.Library.Messaging.Setup.Infrastructure.Startup
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
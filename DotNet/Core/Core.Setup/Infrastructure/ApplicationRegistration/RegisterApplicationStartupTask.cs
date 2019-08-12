using System;
using System.Threading;
using System.Threading.Tasks;
using Gateway.Messages;
using Microsoft.Extensions.DependencyInjection;
using PaladinRogue.Library.Core.Common.Settings;
using PaladinRogue.Library.Core.Setup.Infrastructure.Options;
using PaladinRogue.Library.Core.Setup.Infrastructure.Startup;
using PaladinRogue.Library.Messaging.Common.Dispatchers;
using PaladinRogue.Library.Messaging.Common.Senders;

namespace PaladinRogue.Library.Core.Setup.Infrastructure.ApplicationRegistration
{
    public class RegisterApplicationStartupTask : IStartupTask
    {
        private readonly IServiceProvider _serviceProvider;

        public RegisterApplicationStartupTask(IServiceProvider serviceProvider)
        {
            _serviceProvider = serviceProvider;
        }

        public async Task ExecuteAsync(CancellationToken cancellationToken = default)
        {
            using (IServiceScope scope = _serviceProvider.CreateScope())
            {
                IServiceProvider serviceProvider = scope.ServiceProvider;

                IMessageSender messageSender = serviceProvider.GetRequiredService<IMessageSender>();

                IMessageDispatcher messageDispatcher = serviceProvider.GetRequiredService<IMessageDispatcher>();

                AppSettings appSettings = serviceProvider.GetRequiredOptions<AppSettings>();

                HostSettings hostSettings = serviceProvider.GetRequiredOptions<HostSettings>();

                await messageSender.SendAsync(
                    RegisterApplicationMessage.Create(appSettings.Name, appSettings.SystemName, hostSettings.Urls)
                );

                await messageDispatcher.DispatchMessagesAsync();
            }
        }
    }
}
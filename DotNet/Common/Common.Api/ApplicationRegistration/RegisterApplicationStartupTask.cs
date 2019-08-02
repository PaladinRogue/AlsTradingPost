using System;
using System.Threading;
using System.Threading.Tasks;
using Common.Api.Extensions;
using Common.Messaging.Infrastructure.Dispatchers;
using Common.Messaging.Infrastructure.Senders;
using Common.Messaging.Messages;
using Common.Resources.Settings;
using Common.Setup.Infrastructure.Startup;
using Microsoft.Extensions.DependencyInjection;

namespace Common.Api.ApplicationRegistration
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
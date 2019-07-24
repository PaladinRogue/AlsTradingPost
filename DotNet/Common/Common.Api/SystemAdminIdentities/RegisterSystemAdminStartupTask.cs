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

namespace Common.Api.SystemAdminIdentities
{
    public class RegisterSystemAdminStartupTask : IStartupTask
    {
        private readonly IServiceProvider _serviceProvider;

        public RegisterSystemAdminStartupTask(IServiceProvider serviceProvider)
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

                SystemAdminIdentitySettings systemAdminIdentitySettings = serviceProvider.GetRequiredOptions<SystemAdminIdentitySettings>();

                AppSettings appSettings = serviceProvider.GetRequiredOptions<AppSettings>();

                await messageSender.SendAsync(
                    CreateAdminIdentityMessage.Create(appSettings.SystemName, systemAdminIdentitySettings.Email)
                );

                await messageDispatcher.DispatchMessagesAsync();
            }
        }
    }
}
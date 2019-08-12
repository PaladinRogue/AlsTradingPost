using System;
using System.Threading;
using System.Threading.Tasks;
using Microsoft.Extensions.DependencyInjection;
using PaladinRogue.Authentication.Messages;
using PaladinRogue.Library.Core.Common.Settings;
using PaladinRogue.Library.Core.Setup.Infrastructure.Options;
using PaladinRogue.Library.Core.Setup.Infrastructure.Startup;
using PaladinRogue.Library.Messaging.Common.Dispatchers;
using PaladinRogue.Library.Messaging.Common.Senders;

namespace PaladinRogue.Authentication.Setup.Infrastructure.SystemAdmins
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
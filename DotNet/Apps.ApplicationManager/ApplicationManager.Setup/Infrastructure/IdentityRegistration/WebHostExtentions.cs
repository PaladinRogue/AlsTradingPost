using System;
using ApplicationManager.Setup.Infrastructure.Settings;
using Common.Messaging.Message.Interfaces;
using Common.Messaging.Messages;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Options;

namespace ApplicationManager.Setup.Infrastructure.IdentityRegistration
{
    public static class WebHostExtentions
    {
        public static IWebHost CreateSystemAdminIdentity(this IWebHost webHost)
        {
            IServiceProvider serviceProvider = webHost.Services;

            IMessageBus messageBus = serviceProvider.GetRequiredService<IMessageBus>();

            IOptions<SystemAdminIdentitySettings> systemAdminIdentitySettingsAccessor = serviceProvider.GetRequiredService<IOptions<SystemAdminIdentitySettings>>();
            SystemAdminIdentitySettings systemAdminIdentitySettings = systemAdminIdentitySettingsAccessor.Value;

            messageBus.Publish(new CreateSystemAdminIdentityMessage
            {
                Identifier = systemAdminIdentitySettings.Identifier,
                Password = systemAdminIdentitySettings.Password
            });

            return webHost;
        }
    }
}

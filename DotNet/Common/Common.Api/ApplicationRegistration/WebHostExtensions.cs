using System;
using Common.Messaging.Infrastructure.Dispatchers;
using Common.Messaging.Infrastructure.Senders;
using Common.Messaging.Messages;
using Common.Resources.Settings;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Options;

namespace Common.Api.ApplicationRegistration
{
    public static class WebHostExtensions
    {
        public static IWebHost RegisterApplication(this IWebHost webHost)
        {
            IServiceProvider serviceProvider = webHost.Services;

            IMessageSender messageSender = serviceProvider.GetRequiredService<IMessageSender>();

            IMessageDispatcher messageDispatcher = serviceProvider.GetRequiredService<IMessageDispatcher>();

            IOptions<SystemAdminIdentitySettings> systemAdminIdentitySettingsAccessor =
                serviceProvider.GetRequiredService<IOptions<SystemAdminIdentitySettings>>();

            SystemAdminIdentitySettings systemAdminIdentitySettings = systemAdminIdentitySettingsAccessor.Value;

            IOptions<AppSettings> appSettingsAccessor =
                serviceProvider.GetRequiredService<IOptions<AppSettings>>();

            AppSettings appSettings = appSettingsAccessor.Value;

            messageSender.Send(
                    RegisterApplicationMessage.Create(appSettings.Name, appSettings.SystemName, systemAdminIdentitySettings.Email)
                );

            messageDispatcher.DispatchMessagesAsync();

            return webHost;
        }
    }
}

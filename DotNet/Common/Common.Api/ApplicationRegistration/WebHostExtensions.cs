using System;
using Common.Api.Settings;
using Common.Messaging.Message.Interfaces;
using Common.Messaging.Messages;
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

            IMessageBus messageBus = serviceProvider.GetRequiredService<IMessageBus>();

            IOptions<SystemAdminIdentitySettings> systemAdminIdentitySettingsAccessor =
                serviceProvider.GetRequiredService<IOptions<SystemAdminIdentitySettings>>();

            SystemAdminIdentitySettings systemAdminIdentitySettings = systemAdminIdentitySettingsAccessor.Value;

            IOptions<AppSettings> appSettingsAccessor =
                serviceProvider.GetRequiredService<IOptions<AppSettings>>();

            AppSettings appSettings = appSettingsAccessor.Value;

            messageBus.Publish(RegisterApplicationMessage.Create(appSettings.Name, appSettings.SystemName,
                systemAdminIdentitySettings.Email));

            return webHost;
        }
    }
}

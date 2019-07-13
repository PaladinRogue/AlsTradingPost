using System;
using Common.Api.Extensions;
using Common.Messaging.Infrastructure.Dispatchers;
using Common.Messaging.Infrastructure.Senders;
using Common.Messaging.Messages;
using Common.Resources.Settings;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.DependencyInjection;

namespace Common.Api.ApplicationRegistration
{
    public static class WebHostExtensions
    {
        public static IWebHost RegisterApplication(this IWebHost webHost)
        {
            IServiceProvider serviceProvider = webHost.Services;

            IMessageSender messageSender = serviceProvider.GetRequiredService<IMessageSender>();

            IMessageDispatcher messageDispatcher = serviceProvider.GetRequiredService<IMessageDispatcher>();

            SystemAdminIdentitySettings systemAdminIdentitySettings = serviceProvider.GetRequiredOptions<SystemAdminIdentitySettings>();

            AppSettings appSettings = serviceProvider.GetRequiredOptions<AppSettings>();

            HostSettings hostSettings = serviceProvider.GetRequiredOptions<HostSettings>();

            messageSender.SendAsync(
                    RegisterApplicationMessage.Create(appSettings.Name, appSettings.SystemName, hostSettings.Urls, systemAdminIdentitySettings.Email)
                );

            messageDispatcher.DispatchMessagesAsync();

            return webHost;
        }
    }
}

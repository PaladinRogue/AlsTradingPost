using System;
using System.Threading.Tasks;
using Common.Api.Extensions;
using Common.Messaging.Infrastructure.Dispatchers;
using Common.Messaging.Infrastructure.Senders;
using Common.Messaging.Messages;
using Common.Resources.Settings;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.DependencyInjection;

namespace Common.Api.SystemAdminIdentities
{
    public static class WebHostExtensions
    {
        public static async Task RegisterSystemAdminAsync(this IWebHost webHost)
        {
            using (IServiceScope scope = webHost.Services.CreateScope())
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
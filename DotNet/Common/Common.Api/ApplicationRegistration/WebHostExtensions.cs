using System;
using System.Threading.Tasks;
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
        public static async Task RegisterApplicationAsync(this IWebHost webHost)
        {
            using (IServiceScope scope = webHost.Services.CreateScope())
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
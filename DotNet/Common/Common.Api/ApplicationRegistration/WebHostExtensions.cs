using System;
using Common.Messaging.Message.Interfaces;
using Common.Messaging.Messages;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.DependencyInjection;

namespace Common.Api.ApplicationRegistration
{
    public static class WebHostExtensions
    {
        public static IWebHost RegisterApplication(this IWebHost webHost, string applicationName, string systemName)
        {
            IServiceProvider serviceProvider = webHost.Services;

            IMessageBus messageBus = serviceProvider.GetRequiredService<IMessageBus>();

            messageBus.Publish(new RegisterApplicationMessage
            {
                Name = applicationName,
                SystemName = systemName
            });

            return webHost;
        }
    }
}

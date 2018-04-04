using System;
using Common.Authentication.Enum;
using Message.Broker.Interfaces;
using Message.Messages;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.DependencyInjection;

namespace Common.Resources.Extensions
{
    public static class WebHostExtensions
    {
        public static IWebHost RegisterApplication(this IWebHost webHost, string applicationName, AuthenticationProtocol authenticationProtocols)
        {
            IServiceProvider serviceProvider = webHost.Services;

            IMessageBus messageBus = serviceProvider.GetRequiredService<IMessageBus>();
            messageBus.Publish(new ApplicationCreatedMessage
            {
                Name = applicationName,
                AuthenticationProtocols = authenticationProtocols
            });

            return webHost;
        }
    }
}

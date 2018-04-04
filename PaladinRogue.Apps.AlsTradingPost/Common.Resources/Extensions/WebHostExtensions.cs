using System;
using Common.Authentication.Enum;
using Common.Messaging.Interfaces;
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

            IMessageSender messageSender = serviceProvider.GetService<IMessageSender>();
            messageSender.SendAsync(new ApplicationCreatedMessage
            {
                Name = applicationName,
                AuthenticationProtocols = authenticationProtocols
            });

            return webHost;
        }

        public static IWebHost CheckForApplications(this IWebHost webHost)
        {
            IServiceProvider serviceProvider = webHost.Services;

            IMessageReceiver messageReceiver = serviceProvider.GetService<IMessageReceiver>();
            messageReceiver.Receive();

            return webHost;
        }
    }
}

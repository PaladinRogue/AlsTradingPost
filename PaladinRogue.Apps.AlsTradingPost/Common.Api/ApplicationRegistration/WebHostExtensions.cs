using System;
using Common.Messaging.Message.Interfaces;
using Common.Messaging.Messages;
using Common.Resources.Authentication;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.DependencyInjection;

namespace Common.Api.ApplicationRegistration
{
    public static class WebHostExtensions
    {
        public static IWebHost RegisterApplication(this IWebHost webHost, string applicationName, string authenticationEndpoint, AuthenticationProtocol authenticationProtocols)
        {
            IServiceProvider serviceProvider = webHost.Services;

            IMessageBus messageBus = serviceProvider.GetRequiredService<IMessageBus>();

            messageBus.Publish(new ApplicationCreatedMessage
            {
                Name = applicationName,
                AuthenticationProtocols = authenticationProtocols,
                AuthenticationEndpoint = authenticationEndpoint
            });

            return webHost;
        }
    }
}

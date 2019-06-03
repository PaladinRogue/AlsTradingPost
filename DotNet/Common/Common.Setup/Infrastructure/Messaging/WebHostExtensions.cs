using System;
using Common.Messaging.Message.Interfaces;
using Common.Messaging.Subscribers;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.DependencyInjection;

namespace Common.Setup.Infrastructure.Messaging
{
    public static class WebHostExtensions
    {
        public static IWebHost InitialiseMessaging(this IWebHost webHost)
        {
            IServiceProvider serviceProvider = webHost.Services;

            IMessageSubscriberFactory messageSubscriberFactory = serviceProvider.GetRequiredService<IMessageSubscriberFactory>();

            messageSubscriberFactory.Initialise();

            return webHost;
        }
    }
}

using System;
using Common.Messaging.Infrastructure.Subscribers;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.DependencyInjection;

namespace Common.Setup.Infrastructure.Messaging
{
    public static class WebHostExtensions
    {
        public static IWebHost InitialiseMessaging(this IWebHost webHost)
        {
            using (IServiceScope scope = webHost.Services.CreateScope())
            {
                IServiceProvider serviceProvider = scope.ServiceProvider;
                IMessageSubscriberFactory messageSubscriberFactory = serviceProvider.GetRequiredService<IMessageSubscriberFactory>();

                messageSubscriberFactory.Initialise();

                return webHost;
            }
        }
    }
}

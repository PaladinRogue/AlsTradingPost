using ApplicationManager.ApplicationServices.Subscribers;
using Common.Messaging.Infrastructure.Subscribers;
using Microsoft.Extensions.DependencyInjection;

namespace ApplicationManager.Setup.Infrastructure.Messaging
{
    public static class ServiceCollectionExtensions
    {
        public static IServiceCollection RegisterMessageSubscribers(this IServiceCollection services)
        {
            return services
                .AddScoped<IMessageSubscriber, SendNotificationMessageSubscriber>()
                .AddScoped<IMessageSubscriber, CreateAdminIdentityMessageSubscriber>()
                .AddScoped<IMessageSubscriber, AdminIdentityCreatedMessageSubscriber>();
        }
    }
}
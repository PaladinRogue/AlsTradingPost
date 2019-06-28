using ApplicationManager.ApplicationServices.Subscribers;
using Common.Messaging.Infrastructure.Subscribers;
using Microsoft.Extensions.DependencyInjection;

namespace ApplicationManager.Setup
{
    public static class MessageRegistration
    {
	    public static void RegisterSubscribers(IServiceCollection services)
	    {
	        services.AddScoped<IMessageSubscriber, RegisterApplicationMessageSubscriber>();
	        services.AddScoped<IMessageSubscriber, SendNotificationMessageSubscriber>();
	        services.AddScoped<IMessageSubscriber, CreateAdminIdentityMessageSubscriber>();
        }
	}
}

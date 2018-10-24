using ApplicationManager.ApplicationServices.Applications;
using Common.Messaging.Message.Interfaces;
using Microsoft.Extensions.DependencyInjection;

namespace ApplicationManager.Setup
{
    public static class MessageRegistration
    {
	    public static void RegisterSubscribers(IServiceCollection services)
	    {
	        services.AddScoped<IMessageSubscriber, RegisterApplicationMessageSubscriber>();
        }
	}
}

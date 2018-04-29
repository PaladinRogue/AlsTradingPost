using Authentication.Application.Application;
using Common.Messaging.Message.Interfaces;
using Microsoft.Extensions.DependencyInjection;

namespace Authentication.Setup
{
    public static class MessageRegistration
    {
	    public static void RegisterSubscribers(IServiceCollection services)
	    {
	        services.AddScoped<IMessageSubscriber, ApplicationCreatedMessageSubscriber>();
	    }
	}
}

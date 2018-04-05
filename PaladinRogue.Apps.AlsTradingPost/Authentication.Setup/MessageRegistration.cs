using Authentication.Application.Application;
using Common.Messaging.Message.Interfaces;
using Microsoft.Extensions.DependencyInjection;

namespace Authentication.Setup
{
    public static class MessageRegistration
    {
	    public static void RegisterSubscribers(IServiceCollection services)
	    {
		    Common.Setup.MessageRegistration.RegisterRabbitMqMessaging(services);

	        services.AddSingleton<IMessageSubscriber, ApplicationCreatedMessageSubscriber>();
	    }
	}
}

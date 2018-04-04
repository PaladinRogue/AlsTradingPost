using Microsoft.Extensions.DependencyInjection;

namespace AlsTradingPost.Setup
{
    public static class MessageRegistration
    {
	    public static void RegisterSubscribers(IServiceCollection services)
	    {
		    Common.Setup.MessageRegistration.RegisterRabbitMqMessaging(services);
	    }
	}
}

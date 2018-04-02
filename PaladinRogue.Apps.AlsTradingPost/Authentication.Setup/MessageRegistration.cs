using Microsoft.Extensions.DependencyInjection;

namespace Authentication.Setup
{
    public static class MessageRegistration
    {
	    public static void RegisterSubscribers(IServiceCollection services)
	    {
		    Common.Setup.MessageRegistration.RegisterMessaging(services);
	    }
	}
}

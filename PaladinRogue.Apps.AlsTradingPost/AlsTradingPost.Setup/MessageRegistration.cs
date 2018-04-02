using Microsoft.Extensions.DependencyInjection;

namespace AlsTradingPost.Setup
{
    public static class MessageRegistration
    {
	    public static void RegisterHandlers(IServiceCollection services)
	    {
		    Common.Setup.MessageRegistration.RegisterMessaging(services);
	    }
	}
}

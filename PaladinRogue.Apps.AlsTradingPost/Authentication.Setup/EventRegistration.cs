using Microsoft.Extensions.DependencyInjection;

namespace Authentication.Setup
{
    public static class EventRegistration
    {
	    public static void RegisterHandlers(IServiceCollection services)
	    {
		    Common.Setup.EventRegistration.RegisterEventHandling(services);
	    }
	}
}

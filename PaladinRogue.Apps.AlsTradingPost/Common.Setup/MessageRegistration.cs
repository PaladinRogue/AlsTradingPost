using Message.Broker;
using Message.Broker.Interfaces;
using Microsoft.Extensions.DependencyInjection;

namespace Common.Setup
{
    public static class MessageRegistration
    {
	    public static void RegisterMessaging(IServiceCollection services)
	    {
			services.AddSingleton<MessageHandler>();
		    services.AddSingleton<IMessageReceiver>(p => p.GetService<MessageHandler>());
		    services.AddSingleton<IMessageReceiver>(p => p.GetService<MessageHandler>());

		    services.AddSingleton<IMessageSubscribers, MessageSubscribers>();
		    services.AddScoped<IMessages, Messages>();
			services.AddScoped<IMessageDispatcher, MessageDispatcher>();
	    }
    }
}

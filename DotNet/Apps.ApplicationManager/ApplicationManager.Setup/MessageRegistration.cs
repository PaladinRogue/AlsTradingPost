﻿using ApplicationManager.ApplicationServices.Applications.Subscribers;
using ApplicationManager.ApplicationServices.Notifications.Subscribers;
using ApplicationManager.ApplicationServices.Users.Subscribers;
using Common.Messaging.Message.Interfaces;
using Microsoft.Extensions.DependencyInjection;

namespace ApplicationManager.Setup
{
    public static class MessageRegistration
    {
	    public static void RegisterSubscribers(IServiceCollection services)
	    {
	        services.AddScoped<IMessageSubscriber, RegisterApplicationMessageSubscriber>();
	        services.AddScoped<IMessageSubscriber, SendNotificationMessageSubscriber>();
	        services.AddScoped<IMessageSubscriber, AdminIdentityCreatedMessageSubscriber>();
        }
	}
}

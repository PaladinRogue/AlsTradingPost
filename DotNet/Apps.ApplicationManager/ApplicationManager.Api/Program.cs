﻿using ApplicationManager.Setup.Infrastructure.IdentityRegistration;
using Common.Api.ApplicationRegistration;
using Common.Setup.Infrastructure.Messaging;
using Common.Setup.Infrastructure.Persistence;
using Microsoft.AspNetCore;
using Microsoft.AspNetCore.Hosting;

namespace ApplicationManager.Api
{
    public class Program
    {
	    public static void Main(string[] args)
	    {
		    CreateWebHostBuilder(args)
			    .Build()
			    .ApplyMigrations()
		        .InitialiseMessaging()
		        .RegisterApplication("Application manager", "apps")
		        .CreateSystemAdminIdentity()
                .Run();
	    }

	    public static IWebHostBuilder CreateWebHostBuilder(string[] args) =>
            WebHost.CreateDefaultBuilder(args)
                .UseStartup<Startup>()
	            .UseKestrel()
	            .UseIISIntegration();
    }
}
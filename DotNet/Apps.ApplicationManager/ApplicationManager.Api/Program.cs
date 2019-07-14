﻿using System.IO;
using System.Threading.Tasks;
using Common.Api.ApplicationRegistration;
using Common.Api.SystemAdminIdentities;
using Common.Setup.Infrastructure.Messaging;
using Common.Setup.Infrastructure.Persistence;
using Microsoft.AspNetCore;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;

namespace ApplicationManager.Api
{
    public class Program
    {
        public static async Task Main(string[] args)
        {
            IWebHost builder =
                CreateWebHostBuilder(args)
                    .Build()
                    .ApplyMigrations()
                    .InitialiseMessaging();

            await builder.RegisterApplicationAsync();
            await builder.RegisterSystemAdminAsync();

            builder.Run();
        }

        public static IWebHostBuilder CreateWebHostBuilder(string[] args)
        {
            IConfigurationBuilder builder = new ConfigurationBuilder()
                .SetBasePath(Directory.GetCurrentDirectory())
                .AddJsonFile("hostSettings.json", false)
                .AddEnvironmentVariables();

            return WebHost.CreateDefaultBuilder(args)
                .UseStartup<Startup>()
                .UseConfiguration(builder.Build());
        }
    }
}
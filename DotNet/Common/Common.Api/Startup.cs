﻿using Common.Api.Builders.Resource;
using Common.Api.Links;
using Common.Api.Routing;
using Common.Api.Settings;
using Common.Resources.Settings;
using Common.Setup;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Newtonsoft.Json;
using Newtonsoft.Json.Serialization;
using NodaTime;

namespace Common.Api
{
    public abstract class Startup
    {
        public IHostingEnvironment Environment { get; }
        public IConfiguration Configuration { get; }

        public Startup(IHostingEnvironment environment)
        {
            IConfigurationBuilder builder = new ConfigurationBuilder()
                .SetBasePath(environment.ContentRootPath)
                .AddJsonFile("appsettings.json", optional: false, reloadOnChange: true)
                .AddJsonFile("secrets.json", optional: false, reloadOnChange: true)
                .AddJsonFile("hostsettings.json", optional: false, reloadOnChange: true)
                .AddEnvironmentVariables();

            Configuration = builder.Build();
            Environment = environment;
        }

        public void CommonConfigureServices(IServiceCollection services)
        {
            services.AddMvc(options =>
            {
                options.Conventions.Add(new ApiExplorerVisibilityEnabledConvention());
            })
                .AddJsonOptions(options =>
                {
                    options.SerializerSettings.ContractResolver = new CamelCasePropertyNamesContractResolver();
                    options.SerializerSettings.NullValueHandling = NullValueHandling.Ignore;
                })
                .SetCompatibilityVersion(CompatibilityVersion.Version_2_2);

            services.Configure<ProxySettings>(Configuration.GetSection(nameof(ProxySettings)));
            services.Configure<AppSettings>(Configuration.GetSection(nameof(AppSettings)));
            services.Configure<HostSettings>(Configuration);
            services.Configure<MessagingBusSettings>(Configuration.GetSection(nameof(MessagingBusSettings)));

            services.AddSingleton<ILinkBuilder, DefaultLinkBuilder>();
            services.AddSingleton<IResourceBuilder, DefaultResourceBuilder>();
            services.AddSingleton<IPagingLinkBuilder, DefaultPagingLinkBuilder>();
            services.AddSingleton<ISortingLinkBuilder, DefaultSortingLinkBuilder>();

            services.AddSingleton<IClock>(SystemClock.Instance);

            EventRegistration.RegisterEventHandling(services);
            MessageRegistration.RegisterRabbitMqMessaging(services);
            ServiceRegistration.RegisterProviders(services);
            ServiceRegistration.RegisterServices(services);
            ServiceRegistration.RegisterAuthorisation(services);

            DataProtectionRegistration.Register(Configuration, services);
        }
    }
}
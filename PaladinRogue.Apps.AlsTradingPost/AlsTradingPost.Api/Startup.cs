using System;
using AlsTradingPost.Setup;
using AlsTradingPost.Setup.Infrastructure.Settings;
using AutoMapper;
using Common.Api.Extensions;
using Common.Api.Routing;
using Common.Api.Settings;
using Common.Domain.DomainEvents.Interfaces;
using Common.Resources.Logging;
using Common.Setup.Settings;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Formatters;
using Microsoft.AspNetCore.Rewrite;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;

namespace AlsTradingPost.Api
{
    public class Startup
    {
        public Startup(IHostingEnvironment environment)
        {
            IConfigurationBuilder builder = new ConfigurationBuilder()
                .SetBasePath(environment.ContentRootPath)
                .AddJsonFile("appsettings.json", optional: false, reloadOnChange: true)
                .AddJsonFile("secrets.json", optional: false, reloadOnChange: true);

            Configuration = builder.Build();
            Environment = environment;
        }

        private IHostingEnvironment Environment { get; }
        private IConfiguration Configuration { get; }

        public IServiceProvider ConfigureServices(IServiceCollection services)
        {
            services.AddMvc(options =>
            {
                options.Conventions.Add(new ApiExplorerVisibilityEnabledConvention());
            });

            services.Configure<MvcOptions>(options =>
            {
                options.UseCamelCaseJsonOutputFormatter<JsonOutputFormatter>(services)
                    .UseConcurrencyFilter()
                    .UseAppAccessAuthorizeFilter();

                if (!Environment.IsDevelopment())
                {
                    options.RequireHttps();
                }
            });
            
            services.Configure<ProxySettings>(Configuration.GetSection(nameof(ProxySettings)));
            services.Configure<AppSettings>(Configuration.GetSection(nameof(AppSettings)));
            services.Configure<MessagingBusSettings>(Configuration.GetSection(nameof(MessagingBusSettings)));
            services.Configure<FacebookSettings>(Configuration.GetSection(nameof(FacebookSettings)));

            JwtRegistration.RegisterOptions(Configuration, services);
            EventRegistration.RegisterHandlers(services);

            MessageRegistration.RegisterSubscribers(services);
            ServiceRegistration.RegisterValidators(services);
            ServiceRegistration.RegisterApplicationServices( services);
            ServiceRegistration.RegisterDomainServices(services);
            ServiceRegistration.RegisterPersistenceServices(Configuration, services);

            ServiceRegistration.RegisterProviders(services);

            services.AddAutoMapper(MappingRegistration.RegisterMappers);

            return services.BuildServiceProvider();
        }

        public void Configure(IApplicationBuilder app,
            ILoggerFactory loggerFactory,
            IDomainEventHandlerFactory domainEventHandlerFactory)
        {
            domainEventHandlerFactory.Initialise();

            loggerFactory.AddLog4Net();

            if (Environment.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }
            else
            {
                RewriteOptions options = new RewriteOptions()
                    .AddRedirectToHttps();
                app.UseRewriter(options);
            }

            MiddlewareRegistration.Register(app);
            
            app.UseAuthentication();
            app.UseMvc();
        }
    }
}

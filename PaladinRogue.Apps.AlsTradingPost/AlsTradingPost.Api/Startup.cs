using System;
using AlsTradingPost.Api.Factories;
using AlsTradingPost.Setup;
using AlsTradingPost.Setup.Settings;
using AutoMapper;
using Common.Api.Extensions;
using Common.Api.Factories.Interfaces;
using Common.Api.Settings;
using Common.Domain.DomainEvents.Interfaces;
using Common.Resources.Logging;
using Common.Resources.Providers;
using Common.Resources.Providers.Interfaces;
using Common.Setup.Settings;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Rewrite;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using MappingRegistration = AlsTradingPost.Api.Mappings.MappingRegistration;

namespace AlsTradingPost.Api
{
    public class Startup
    {
        public Startup(IConfiguration config)
        {
            Configuration = config;
        }

        public IConfiguration Configuration { get; }

        // This method gets called by the runtime. Use this method to add services to the container.
        public IServiceProvider ConfigureServices(IServiceCollection services)
        {
            services.AddMvc();

            services.Configure<MvcOptions>(options =>
            {
                options.UseCustomJsonOutputFormatter()
                    .UseConcurrencyFilter()
                    .UseAppAccessAuthorizeFilter()
                    .RequireHttps();
            });

            services.AddScoped<IClaimsFactory, ClaimsFactory>();
            services.AddScoped<ICurrentIdentityProvider, CurrentIdentityProvider>();

            services.Configure<ProxySettings>(Configuration.GetSection(nameof(ProxySettings)));
            services.Configure<AppSettings>(Configuration.GetSection(nameof(AppSettings)));
            services.Configure<MessagingBusSettings>(Configuration.GetSection(nameof(MessagingBusSettings)));
            services.Configure<FacebookSettings>(Configuration.GetSection(nameof(FacebookSettings)));

            JwtRegistration.RegisterOptions(Configuration, services);
            EventRegistration.RegisterHandlers(services);
            MessageRegistration.RegisterSubscribers(services);
            ServiceRegistration.RegisterServices(Configuration, services);
            ServiceRegistration.RegisterProviders(Configuration, services);

            services.AddAutoMapper(MappingRegistration.RegisterMappers);

            return services.BuildServiceProvider();
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app,
            IHostingEnvironment env,
            ILoggerFactory loggerFactory,
            IDomainEventHandlerFactory domainEventHandlerFactory)
        {
            domainEventHandlerFactory.Initialise();

            loggerFactory.AddLog4Net();

            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }

            var options = new RewriteOptions()
                .AddRedirectToHttps();
            app.UseRewriter(options);

            MiddlewareRegistration.Register(app);

            app.UseAuthentication();
            app.UseMvc();
        }
    }
}

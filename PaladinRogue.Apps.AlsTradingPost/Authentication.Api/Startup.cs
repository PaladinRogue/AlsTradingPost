using Authentication.Api.Factories;
using Authentication.Api.Factories.Interfaces;
using Common.Api.Formatters;
using Authentication.Setup;
using Authentication.Setup.Settings;
using AutoMapper;
using Common.Api.Filters;
using Common.Api.Settings;
using Common.Domain.DomainEvents.Interfaces;
using Common.Resources.Logging;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Formatters;
using Microsoft.AspNetCore.Rewrite;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using Newtonsoft.Json;
using Newtonsoft.Json.Serialization;
using MappingRegistration = Authentication.Api.Mappings.MappingRegistration;

namespace Authentication.Api
{
	public class Startup
	{
		public Startup(IConfiguration configuration)
		{
			Configuration = configuration;
		}

		public IConfiguration Configuration { get; }

		// This method gets called by the runtime. Use this method to add services to the container.
		public void ConfigureServices(IServiceCollection services)
		{
			services.AddMvc(UseCustomJsonOutputFormatter);

			services.Configure<MvcOptions>(options =>
			{
				options.Filters.Add(new RequireHttpsAttribute());
				options.Filters.Add(new ConcurrencyActionFilter());
			});

			services.AddSingleton<IClaimsFactory, ClaimsFactory>();

			services.Configure<ProxySettings>(Configuration.GetSection(nameof(ProxySettings)));
			services.Configure<AppSettings>(Configuration.GetSection(nameof(AppSettings)));
			services.Configure<FacebookAuthSettings>(Configuration.GetSection(nameof(FacebookAuthSettings)));

			JwtRegistration.RegisterOptions(Configuration, services);

			EventRegistration.RegisterHandlers(services);

			ServiceRegistration.RegisterServices(Configuration, services);
			ServiceRegistration.RegisterProviders(Configuration, services);

			services.AddAutoMapper(MappingRegistration.RegisterMappers);
		}

		// This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
		public void Configure(IApplicationBuilder app, IHostingEnvironment env, ILoggerFactory loggerFactory,
			IDomainEventHandlers domainEventHandlers)
		{
			domainEventHandlers.Initialise();

			loggerFactory.AddLog4Net();

			if (env.IsDevelopment())
			{
				app.UseDeveloperExceptionPage();
			}

			var options = new RewriteOptions()
				.AddRedirectToHttps();
			app.UseRewriter(options);

			MiddlewareRegistration.Register(app);

			app.UseMvc();
		}

		public static void UseCustomJsonOutputFormatter(MvcOptions options)
		{
			// Remove any json output formatter 
			options.OutputFormatters.RemoveType<JsonOutputFormatter>();

			// Add custom json output formatter 
			var jsonSerializerSettings = new JsonSerializerSettings
			{
				ContractResolver = new CamelCasePropertyNamesContractResolver()
			};
			options.OutputFormatters.Add(new CustomJsonOutputFormatter(jsonSerializerSettings,
				System.Buffers.ArrayPool<char>.Shared));
		}
	}
}
using AlsTradingPost.Api.Formatters;
using AlsTradingPost.Setup;
using AutoMapper;
using Common.Api.Filters;
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
using MappingRegistration = AlsTradingPost.Api.Mappings.MappingRegistration;

namespace AlsTradingPost.Api
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

            ServiceRegistration.RegisterServices(Configuration, services);
            ServiceRegistration.RegisterProviders(Configuration, services);

            services.AddAutoMapper(MappingRegistration.RegisterMappers);
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IHostingEnvironment env, ILoggerFactory loggerFactory)
        {
            loggerFactory.AddLog4Net();

            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }

            var options = new RewriteOptions()
                .AddRedirectToHttps();

            MiddlewareRegistration.RegisterTransactionPerRequest(app);

            app.UseRewriter(options);

            app.UseMvc();
        }

        public static void UseCustomJsonOutputFormatter(MvcOptions options)
        {
            // Remove any json output formatter 
            options.OutputFormatters.RemoveType<JsonOutputFormatter>();

            // Add custom json output formatter 
            JsonSerializerSettings jsonSerializerSettings = new JsonSerializerSettings
            {
                ContractResolver = new CamelCasePropertyNamesContractResolver()
            };
            options.OutputFormatters.Add(new CustomJsonOutputFormatter(jsonSerializerSettings, System.Buffers.ArrayPool<char>.Shared));
        }
    }
}

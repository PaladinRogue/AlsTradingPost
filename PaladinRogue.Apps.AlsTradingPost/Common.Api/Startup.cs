using Common.Api.Pagination;
using Common.Api.QueryString;
using Common.Api.Routing;
using Common.Api.Settings;
using Common.Api.Sorting;
using Common.Setup;
using Common.Setup.Settings;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

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
                .AddEnvironmentVariables();

            Configuration = builder.Build();
            Environment = environment;
        }
        
        public void CommonConfigureServices(IServiceCollection services)
        {
            services.AddMvc(options =>
            {
                options.Conventions.Add(new ApiExplorerVisibilityEnabledConvention());
                options.Conventions.Add(new QueryStringSortConvention());
                options.ModelBinderProviders.Insert(0, new QueryStringSortModelBinderProvider());
                options.ModelBinderProviders.Insert(0, new QueryStringPageSizeModelBinderProvider());
                options.ModelBinderProviders.Insert(0, new QueryStringPageOffsetModelBinderProvider());
            }).SetCompatibilityVersion(CompatibilityVersion.Version_2_1);

            services.Configure<ProxySettings>(Configuration.GetSection(nameof(ProxySettings)));
            services.Configure<AppSettings>(Configuration.GetSection(nameof(AppSettings)));
            services.Configure<MessagingBusSettings>(Configuration.GetSection(nameof(MessagingBusSettings)));

            EventRegistration.RegisterEventHandling(services);
            MessageRegistration.RegisterRabbitMqMessaging(services);
            ServiceRegistration.RegisterProviders(services);
            ServiceRegistration.RegisterServices(services);
            DataProtectionRegistration.Register(Configuration, services);
            
            Common.Authentication.Setup.ServiceRegistration.RegisterDomainServices(services);
            Common.Authentication.Setup.ServiceRegistration.RegisterProviders(services);
        }
    }
}
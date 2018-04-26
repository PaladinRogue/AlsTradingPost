using Authentication.Application.Identity;
using Authentication.Application.Identity.Interfaces;
using Authentication.Domain.ApplicationServices;
using Authentication.Domain.ApplicationServices.Interfaces;
using Authentication.Domain.IdentityServices;
using Authentication.Domain.IdentityServices.Interfaces;
using Authentication.Domain.Persistence;
using Authentication.Persistence;
using Authentication.Persistence.Repositories;
using Common.Api.Builders;
using Common.Api.Builders.Resource;
using Common.Api.Builders.Template;
using Common.Api.Encryption;
using Common.Api.Encryption.Interfaces;
using Common.Api.HttpClient;
using Common.Api.HttpClient.Interfaces;
using Common.Api.Links;
using Common.Api.Routing;
using Common.Domain.Concurrency;
using Common.Domain.Concurrency.Interfaces;
using Common.Domain.Concurrency.Services;
using Common.Domain.Concurrency.Services.Interfaces;
using Common.Resources.Transactions;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Persistence.EntityFramework.Transactions;

namespace Authentication.Setup
{
    public class ServiceRegistration
    {
	    public static void RegisterBuilders(IServiceCollection services)
	    {
		    services.AddSingleton<IBuildHelper, BuildHelper>();
		    services.AddSingleton<ILinkBuilder, DefaultLinkBuilder>();
		    services.AddSingleton<IResourceTemplateBuilder, ResourceTemplateBuilder>();
	    }
	    
        public static void RegisterServices(IConfiguration configuration, IServiceCollection services)
        {
	        services.AddSingleton<IEncryptionFactory, EncryptionFactory>();
	        services.AddSingleton<IHttpClientFactory, HttpClientFactory>();

            services.AddScoped(typeof(IConcurrencyQueryService<>), typeof(ConcurrencyQueryService<>));

			services.AddScoped<IIdentityApplicationService, IdentityApplicationService>();

			services.AddScoped<IIdentityQueryService, IdentityQueryService>();
			services.AddScoped<IIdentityCommandService, IdentityCommandService>();

			services.AddScoped<IIdentityRepository, IdentityRepository>();

			services.AddScoped<IApplicationQueryService, ApplicationQueryService>();
			services.AddScoped<IApplicationCommandService, ApplicationCommandService>();

			services.AddScoped<IApplicationRepository, ApplicationRepository>();

            services.AddEntityFrameworkSqlServer().AddOptions()
                .AddDbContext<AuthenticationDbContext>(options => options.UseSqlServer(configuration.GetConnectionString("Default")));
            services.AddScoped<DbContext>(sp => sp.GetRequiredService<AuthenticationDbContext>());
            services.AddTransient<ITransactionFactory, TransactionFactory>();
		}

        public static void RegisterProviders(IServiceCollection services)
        {
            services.AddSingleton<IConcurrencyVersionProvider, ConcurrencyVersionProvider>();
            services.AddSingleton<IRouteProvider<bool>, DefaultRouteProvider>();
        }
    }
}

using Authentication.Application.Application;
using Authentication.Application.Application.Interfaces;
using Authentication.Application.Authentication;
using Authentication.Application.Authentication.Interfaces;
using Authentication.Application.Authentication.Models;
using Authentication.Application.Authentication.Validators;
using Authentication.Domain.ApplicationServices;
using Authentication.Domain.ApplicationServices.Interfaces;
using Authentication.Domain.IdentityServices;
using Authentication.Domain.IdentityServices.Interfaces;
using Authentication.Persistence;
using Authentication.Setup.Infrastructure.Authorisation;
using Authentication.Setup.Infrastructure.Links;
using Common.Api.HttpClient;
using Common.Api.HttpClient.Interfaces;
using Common.Api.Links;
using Common.Api.Routing;
using Common.Application.Authorisation;
using Common.Application.Authorisation.Policy;
using Common.Application.Transactions;
using Common.Domain.Persistence;
using FluentValidation;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Persistence.EntityFramework.Infrastructure.Transactions;
using Persistence.EntityFramework.Repositories;

namespace Authentication.Setup
{
    public class ServiceRegistration
    {
	    public static void RegisterBuilders(IServiceCollection services)
	    {
		    services.AddSingleton<ILinkFactory, DefaultLinkFactory>();
	    }

	    public static void RegisterValidators(IServiceCollection services)
	    {
		    ValidatorOptions.LanguageManager.Enabled = false;

		    services.AddTransient<IValidator<RefreshTokenAdto>, RefreshTokenValidator>();
		    services.AddTransient<IValidator<LoginAdto>, LoginValidator>();
	    }
	    
        public static void RegisterApplicationServices(IServiceCollection services)
        {
	        services.AddSingleton<IHttpClientFactory, HttpClientFactory>();
            
            services.AddScoped<ISecure<IAuthenticationApplicationService>, AuthenticationSecurityApplicationService>();
            services.AddScoped<IAuthenticationApplicationService, AuthenticationApplicationService>();

            services.AddScoped<IApplicationApplicationKernalService, ApplicationApplicationKernalService>();
		}
	    
        public static void RegisterDomainServices(IServiceCollection services)
        {
			services.AddScoped<IIdentityDomainService, IdentityDomainService>();

			services.AddScoped<IApplicationDomainService, ApplicationDomainService>();
		}
	    
        public static void RegisterPersistenceServices(IConfiguration configuration, IServiceCollection services)
        {
            services.AddScoped(typeof(IRepository<>), typeof(Repository<>));


            services.AddEntityFrameworkSqlServer().AddOptions()
                .AddDbContext<AuthenticationDbContext>(options => options.UseSqlServer(configuration.GetConnectionString("Default")));
            services.AddScoped<DbContext>(sp => sp.GetRequiredService<AuthenticationDbContext>());
            services.AddScoped<ITransactionManager, EntityFrameworkTransactionManager>();
        }

        public static void RegisterProviders(IServiceCollection services)
        {
            services.AddSingleton<IRouteProvider<bool>, DefaultRouteProvider>();
            services.AddSingleton<FacebookApplicationLinksProvider>();
        }

        public static void RegisterAuthorisation(IServiceCollection services)
        {
            services.AddSingleton<IAuthorisationPolicy, AlwaysAllowAuthorisationPolicy>();
        }
    }
}

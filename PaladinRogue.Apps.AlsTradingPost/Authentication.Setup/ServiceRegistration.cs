using Authentication.Application.Authentication;
using Authentication.Application.Authentication.Interfaces;
using Authentication.Application.Authentication.Models;
using Authentication.Application.Authentication.Validators;
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
using Common.Api.HttpClient;
using Common.Api.HttpClient.Interfaces;
using Common.Api.Links;
using Common.Api.Meta;
using Common.Api.Routing;
using Common.Application.Transactions;
using Common.Authentication.Domain.Persistence;
using FluentValidation;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Persistence.EntityFramework.Infrastructure.Transactions;

namespace Authentication.Setup
{
    public class ServiceRegistration
    {
	    public static void RegisterBuilders(IServiceCollection services)
	    {
		    services.AddSingleton<IMetaBuilder, MetaBuilder>();
		    services.AddSingleton<IBuildHelper, BuildHelper>();
		    services.AddSingleton<ILinkBuilder, DefaultLinkBuilder>();
		    services.AddSingleton<IResourceTemplateBuilder, ResourceTemplateBuilder>();
		    services.AddSingleton<ITemplateBuilder, TemplateBuilder>();
		    services.AddSingleton<IResourceBuilder, ResourceBuilder>();
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
            
            services.AddScoped<IAuthenticationApplicationService, AuthenticationApplicationService>();
		}
	    
        public static void RegisterDomainServices(IServiceCollection services)
        {
			services.AddScoped<IIdentityDomainService, IdentityDomainService>();
			services.AddScoped<IIdentityQueryService, IdentityQueryService>();
			services.AddScoped<IIdentityCommandService, IdentityCommandService>();

			services.AddScoped<IApplicationQueryService, ApplicationQueryService>();
			services.AddScoped<IApplicationCommandService, ApplicationCommandService>();
		}
	    
        public static void RegisterPersistenceServices(IConfiguration configuration, IServiceCollection services)
        {
	        services.AddScoped<IIdentityRepository, IdentityRepository>();
			services.AddScoped<IApplicationRepository, ApplicationRepository>();
			services.AddScoped<ISessionRepository, SessionRepository>();

            services.AddEntityFrameworkSqlServer().AddOptions()
                .AddDbContext<AuthenticationDbContext>(options => options.UseSqlServer(configuration.GetConnectionString("Default")));
            services.AddScoped<DbContext>(sp => sp.GetRequiredService<AuthenticationDbContext>());
            services.AddScoped<ITransactionManager, EntityFrameworkTransactionManager>();
        }

        public static void RegisterProviders(IServiceCollection services)
        {
            services.AddSingleton<IRouteProvider<bool>, DefaultRouteProvider>();
        }
    }
}

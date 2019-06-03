using ApplicationManager.ApplicationServices.Applications;
using ApplicationManager.ApplicationServices.Applications.Interfaces;
using ApplicationManager.ApplicationServices.Identities;
using ApplicationManager.ApplicationServices.Identities.Interfaces;
using ApplicationManager.Domain.Applications;
using ApplicationManager.Domain.Identities;
using ApplicationManager.Domain.Identities.AuthenticationIdentities;
using ApplicationManager.Persistence;
using ApplicationManager.Setup.Infrastructure.Authorisation;
using Common.Api.HttpClient;
using Common.Api.HttpClient.Interfaces;
using Common.Api.Links;
using Common.Api.Routing;
using Common.Application.Authorisation.Policy;
using Common.Application.Transactions;
using Common.Domain.Persistence;
using FluentValidation;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Persistence.EntityFramework.Infrastructure.Transactions;
using Persistence.EntityFramework.Repositories;

namespace ApplicationManager.Setup
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

	        services.AddScoped<IValidator<CreateTwoFactorAuthenticationIdentityDdto>, CreateTwoFactorAuthenticationIdentityValidator>();
	        services.AddScoped<IValidator<ChangeApplicationDdto>, ChangeApplicationValidator>();
	        services.AddScoped<IValidator<CreateApplicationDdto>, CreateApplicationValidator>();
	    }
	    
        public static void RegisterApplicationServices(IServiceCollection services)
        {
	        services.AddSingleton<IHttpClientFactory, HttpClientFactory>();

            services.AddScoped<IRegisterApplicationKernalService, RegisterApplicationKernalService>();
            services.AddScoped<ICreateAdminAuthenticationIdentityKernalService, CreateAdminAuthenticationIdentityKernalService>();
        }
	    
        public static void RegisterDomainServices(IServiceCollection services)
        {
            services.AddScoped<ICreateTwoFactorAuthenticationIdentityCommand, CreateTwoFactorAuthenticationIdentityCommand>();
            services.AddScoped<ICreateIdentityCommand, CreateIdentityCommand>();
            services.AddScoped<IChangeApplicationCommand, ChangeApplicationCommand>();
            services.AddScoped<ICreateApplicationCommand, CreateApplicationCommand>();
        }
	    
        public static void RegisterPersistenceServices(IConfiguration configuration, IServiceCollection services)
        {
            services.AddScoped(typeof(ICommandRepository<>), typeof(Repository<>));
            services.AddScoped(typeof(IQueryRepository<>), typeof(Repository<>));

            services.AddEntityFrameworkSqlServer().AddOptions()
                .AddDbContext<ApplicationManagerDbContext>(options => options.UseSqlServer(configuration.GetConnectionString("Default")));
            services.AddScoped<DbContext>(sp => sp.GetRequiredService<ApplicationManagerDbContext>());
            services.AddScoped<ITransactionManager, EntityFrameworkTransactionManager>();
        }

        public static void RegisterProviders(IServiceCollection services)
        {
            services.AddSingleton<IRouteProvider<bool>, DefaultRouteProvider>();
        }

        public static void RegisterAuthorisation(IServiceCollection services)
        {
            services.AddSingleton<IAuthorisationPolicy, AlwaysAllowAuthorisationPolicy>();
        }
    }
}

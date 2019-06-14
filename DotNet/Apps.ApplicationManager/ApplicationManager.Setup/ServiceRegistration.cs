using ApplicationManager.ApplicationServices.Applications;
using ApplicationManager.ApplicationServices.Applications.Interfaces;
using ApplicationManager.ApplicationServices.Identities;
using ApplicationManager.ApplicationServices.Identities.Admin;
using ApplicationManager.ApplicationServices.Identities.TwoFactor;
using ApplicationManager.ApplicationServices.Notifications;
using ApplicationManager.ApplicationServices.Notifications.Audience;
using ApplicationManager.ApplicationServices.Notifications.Emails;
using ApplicationManager.ApplicationServices.Notifications.Send;
using ApplicationManager.ApplicationServices.Users;
using ApplicationManager.ApplicationServices.Users.CreateAdmin;
using ApplicationManager.Domain.Applications;
using ApplicationManager.Domain.Applications.Commands;
using ApplicationManager.Domain.Applications.Models;
using ApplicationManager.Domain.Applications.Validators;
using ApplicationManager.Domain.Identities;
using ApplicationManager.Domain.Identities.Commands;
using ApplicationManager.Domain.Identities.Models;
using ApplicationManager.Domain.Identities.Queries;
using ApplicationManager.Domain.Identities.Validators;
using ApplicationManager.Domain.Users;
using ApplicationManager.Domain.Users.Commands;
using ApplicationManager.Persistence;
using ApplicationManager.Persistence.Identities;
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

	        services.AddScoped<IValidator<AddTwoFactorAuthenticationIdentityDdto>, CreateTwoFactorAuthenticationIdentityValidator>();
	        services.AddScoped<IValidator<ChangeApplicationDdto>, ChangeApplicationValidator>();
	        services.AddScoped<IValidator<CreateApplicationDdto>, CreateApplicationValidator>();
	        services.AddScoped<IValidator<AddConfirmedPasswordIdentityDdto>, AddConfirmedPasswordIdentityValidator>();
	    }

        public static void RegisterApplicationServices(IServiceCollection services)
        {
	        services.AddSingleton<IHttpClientFactory, HttpClientFactory>();

            services.AddScoped<IRegisterApplicationKernalService, RegisterApplicationKernalService>();
            services.AddScoped<ICreateAdminAuthenticationIdentityKernalService, CreateAdminAuthenticationIdentityKernalService>();
            services.AddScoped<ISendNotificationKernalService, SendNotificationKernalService>();
            services.AddScoped<ISendTwoFactorAuthenticationNotificationKernalService, SendTwoFactorAuthenticationNotificationKernalService>();
            services.AddScoped<ICreateAdminUserApplicationKernalService, CreateAdminUserApplicationKernalService>();
            services.AddScoped<IIdentityApplicationService, IdentityApplicationServiceSecurityDecorator>();
            services.AddScoped<IIdentityApplicationService, IdentityApplicationService>();
        }

        public static void RegisterDomainServices(IServiceCollection services)
        {
            services.AddScoped<IAddTwoFactorAuthenticationIdentityCommand, AddTwoFactorAuthenticationIdentityCommand>();
            services.AddScoped<ICreateIdentityCommand, CreateIdentityCommand>();
            services.AddScoped<IChangeApplicationCommand, ChangeApplicationCommand>();
            services.AddScoped<ICreateApplicationCommand, CreateApplicationCommand>();
            services.AddScoped<ICreateUserCommand, CreateUserCommand>();
            services.AddScoped<ICreateUserCommand, CreateUserCommand>();
            services.AddScoped<IAddConfirmedPasswordIdentityCommand, AddConfirmedPasswordIdentityCommand>();
        }

        public static void RegisterPersistenceServices(IConfiguration configuration, IServiceCollection services)
        {
            services.AddScoped<IGetTwoFactorAuthenticationIdentityByIdentityQuery, GetTwoFactorAuthenticationIdentityByIdentityQuery>();
            services.AddScoped<IPasswordIdentityIdentifierIsUniqueQuery, PasswordIdentityIdentifierIsUniqueQuery>();

            services.AddScoped(typeof(ICommandRepository<>), typeof(Repository<>));
            services.AddScoped(typeof(IQueryRepository<>), typeof(Repository<>));

            services.AddEntityFrameworkSqlServer().AddOptions()
                .AddDbContext<ApplicationManagerDbContext>(options =>
	                options.UseLazyLoadingProxies()
	                .UseSqlServer(configuration.GetConnectionString("Default")));
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

        public static void RegisterNotifications(IServiceCollection services)
        {
	        services.AddScoped<IEmailBuilder, EmailBuilder>();
	        services.AddScoped<IChannelAudienceResolverProvider, ChannelAudienceResolverProvider>();
	        services.AddScoped<IChannelAudienceResolver, TwoFactorAuthenticationEmailChannelResolver>();
	        services.AddScoped<IEmailNotificationSender, LocalDevelopmentEmailNotificationSender>();
        }
    }
}

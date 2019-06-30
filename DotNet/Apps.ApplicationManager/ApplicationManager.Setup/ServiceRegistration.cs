using System.IO;
using ApplicationManager.ApplicationServices.Applications.Register;
using ApplicationManager.ApplicationServices.Authentication;
using ApplicationManager.ApplicationServices.AuthenticationServices;
using ApplicationManager.ApplicationServices.Identities;
using ApplicationManager.ApplicationServices.Identities.CreateAdmin;
using ApplicationManager.ApplicationServices.Identities.TwoFactor;
using ApplicationManager.ApplicationServices.Notifications.Audiences;
using ApplicationManager.ApplicationServices.Notifications.Emails;
using ApplicationManager.ApplicationServices.Notifications.Send;
using ApplicationManager.ApplicationServices.Users.CreateAdmin;
using ApplicationManager.Domain.Applications.Change;
using ApplicationManager.Domain.Applications.Create;
using ApplicationManager.Domain.AuthenticationServices.ChangeClientCredential;
using ApplicationManager.Domain.AuthenticationServices.CreateClientCredential;
using ApplicationManager.Domain.Identities.ChangePassword;
using ApplicationManager.Domain.Identities.CheckPassword;
using ApplicationManager.Domain.Identities.ConfirmIdentity;
using ApplicationManager.Domain.Identities.Create;
using ApplicationManager.Domain.Identities.CreateRefreshToken;
using ApplicationManager.Domain.Identities.ForgotPassword;
using ApplicationManager.Domain.Identities.Login.Password;
using ApplicationManager.Domain.Identities.Login.RefreshToken;
using ApplicationManager.Domain.Identities.Logout;
using ApplicationManager.Domain.Identities.Queries;
using ApplicationManager.Domain.Identities.RegisterPassword;
using ApplicationManager.Domain.Identities.ResendConfirmIdentity;
using ApplicationManager.Domain.Identities.ResetPassword;
using ApplicationManager.Domain.Users.Create;
using ApplicationManager.Persistence;
using ApplicationManager.Persistence.Identities;
using ApplicationManager.Setup.Infrastructure.Authorisation;
using Common.Api.HttpClient;
using Common.Api.HttpClient.Interfaces;
using Common.Api.Links;
using Common.Api.Routing;
using Common.ApplicationServices.Transactions;
using Common.Authorisation;
using Common.Authorisation.Policies;
using Common.Authorisation.Policies.Json;
using Common.Authorisation.Restrictions;
using Common.Domain.Persistence;
using Common.Setup.Infrastructure.Authorisation;
using FluentValidation;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Diagnostics;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Newtonsoft.Json.Linq;
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

        public static void RegisterApplicationServices(IServiceCollection services)
        {
	        services.AddSingleton<IHttpClientFactory, HttpClientFactory>();

            services.AddScoped<IRegisterApplicationKernalService, RegisterApplicationKernalService>();
            services.AddScoped<ICreateAdminAuthenticationIdentityKernalService, CreateAdminAuthenticationIdentityKernalService>();
            services.AddScoped<ISendNotificationKernalService, SendNotificationKernalService>();
            services.AddScoped<ISendTwoFactorAuthenticationNotificationKernalService, SendTwoFactorAuthenticationNotificationKernalService>();
            services.AddScoped<ICreateAdminUserApplicationKernalService, CreateAdminUserApplicationKernalService>();

            services.AddScoped<IdentityApplicationService>();

            services.RegisterApplicationService<IIdentityApplicationService, IdentityApplicationService, IdentityApplicationServiceSecurityDecorator>();

            services.RegisterApplicationService<IAuthenticationApplicationService, AuthenticationApplicationService, AuthenticationApplicationServiceSecurityDecorator>();

            services.RegisterApplicationService<IAuthenticationServiceApplicationService, AuthenticationServiceApplicationService, AuthenticationServiceApplicationServiceSecurityDecorator>();
        }

	    public static void RegisterValidators(IServiceCollection services)
	    {
		    ValidatorOptions.LanguageManager.Enabled = false;

	        services.AddScoped<IValidator<CreateUserCommandDdto>, CreateUserCommandValidator>();
	        services.AddScoped<IValidator<ChangeApplicationDdto>, ChangeApplicationValidator>();
	        services.AddScoped<IValidator<CreateApplicationDdto>, CreateApplicationValidator>();
	        services.AddScoped<IValidator<ResetPasswordCommandDdto>, ResetPasswordValidator>();
	        services.AddScoped<IValidator<PasswordLoginCommandDdto>, PasswordLoginCommandValidator>();
	        services.AddScoped<IValidator<CheckPasswordDdto>, CheckPasswordValidator>();
	        services.AddScoped<IValidator<ChangePasswordCommandDdto>, ChangePasswordValidator>();
	        services.AddScoped<IValidator<RegisterPasswordCommandDdto>, RegisterPasswordValidator>();
	        services.AddScoped<IValidator<ConfirmIdentityCommandDdto>, ConfirmIdentityValidator>();
	        services.AddScoped<IValidator<ForgotPasswordCommandDdto>, ForgotPasswordCommandValidator>();
	        services.AddScoped<IValidator<CreateIdentityCommandDdto>, CreateIdentityValidator>();
	        services.AddScoped<IValidator<RefreshTokenLoginCommandDdto>, RefreshTokenLoginCommandValidator>();
	        services.AddScoped<IValidator<CreateAuthenticationGrantTypeClientCredentialDdto>, CreateAuthenticationGrantTypeClientCredentialValidator>();
	        services.AddScoped<IValidator<ChangeAuthenticationGrantTypeClientCredentialDdto>, ChangeAuthenticationGrantTypeClientCredentialValidator>();
	    }

        public static void RegisterDomainServices(IServiceCollection services)
        {
            services.AddScoped<ICreateIdentityCommand, CreateIdentityCommand>();
            services.AddScoped<IChangeApplicationCommand, ChangeApplicationCommand>();
            services.AddScoped<ICreateApplicationCommand, CreateApplicationCommand>();
            services.AddScoped<ICreateUserCommand, CreateUserCommand>();
            services.AddScoped<ICreateUserCommand, CreateUserCommand>();
            services.AddScoped<IResetPasswordCommand, ResetPasswordCommand>();
            services.AddScoped<IPasswordLoginCommand, PasswordLoginCommand>();
            services.AddScoped<ICheckPasswordCommand, CheckPasswordCommand>();
            services.AddScoped<IChangePasswordCommand, ChangePasswordCommand>();
            services.AddScoped<IRegisterPasswordCommand, RegisterPasswordCommand>();
            services.AddScoped<IConfirmIdentityCommand, ConfirmIdentityCommand>();
            services.AddScoped<IForgotPasswordCommand, ForgotPasswordCommand>();
            services.AddScoped<ICreateRefreshTokenCommand, CreateRefreshTokenCommand>();
            services.AddScoped<ILogoutCommand, LogoutCommand>();
            services.AddScoped<IResendConfirmIdentityCommand, ResendConfirmIdentityCommand>();
            services.AddScoped<IRefreshTokenLoginCommand, RefreshTokenLoginCommand>();
            services.AddScoped<ICreateAuthenticationGrantTypeClientCredentialCommand, CreateAuthenticationGrantTypeClientCredentialCommand>();
            services.AddScoped<IChangeAuthenticationGrantTypeClientCredentialCommand, ChangeAuthenticationGrantTypeClientCredentialCommand>();
        }

        public static void RegisterPersistenceServices(IConfiguration configuration, IServiceCollection services)
        {
	        services.AddScoped<IGetTwoFactorAuthenticationIdentityByIdentityQuery, GetTwoFactorAuthenticationIdentityByIdentityQuery>();
	        services.AddScoped<IPasswordIdentityIdentifierIsUniqueQuery, PasswordIdentityIdentifierIsUniqueQuery>();
	        services.AddScoped<IGetIdentityByIdentifierAndPasswordQuery, GetIdentityByIdentifierAndPasswordQuery>();
	        services.AddScoped<IGetIdentityByEmailAddressQuery, GetIdentityByEmailAddressQuery>();
	        services.AddScoped<IGetIdentityByForgotPasswordTokenQuery, GetIdentityByForgotPasswordTokenQuery>();
	        services.AddScoped<IGetIdentityBySessionQuery, GetIdentityBySessionQuery>();

	        services.AddScoped(typeof(ICommandRepository<>), typeof(Repository<>));
	        services.AddScoped(typeof(IQueryRepository<>), typeof(Repository<>));

	        services.AddEntityFrameworkSqlServer().AddOptions()
		        .AddDbContext<ApplicationManagerDbContext>(options =>
			        options.UseLazyLoadingProxies()
				        .ConfigureWarnings(warnings => warnings.Throw(RelationalEventId.QueryClientEvaluationWarning))
				        .UseSqlServer(configuration.GetConnectionString("Default")));
	        services.AddScoped<DbContext>(sp => sp.GetRequiredService<ApplicationManagerDbContext>());
	        services.AddScoped<ITransactionManager, EntityFrameworkTransactionManager>();
        }

        public static void RegisterProviders(IServiceCollection services)
        {
            services.AddSingleton<IRouteProvider<bool>, DefaultRouteProvider>();
            services.AddSingleton<IAbsoluteRouteProvider, DefaultAbsoluteRouteProvider>();
        }

        public static void RegisterAuthorisation(IServiceCollection services)
        {
	        services.AddSingleton<ICurrentIdentityProvider, CurrentIdentityProvider>();
	        services.AddSingleton<IAuthorisationPolicy, JsonAuthorisationPolicy>();
	        services.AddSingleton<ISelfProvider, SelfIdentityProvider>();
	        services.AddSingleton<IAuthorisationRestrictionProvider, AuthorisationRestrictionProvider>();
	        services.AddSingleton<IJsonAuthorisationPolicyProvider>(s => new JsonAuthorisationPolicyProvider(JObject.Parse(File.ReadAllText("authorisationPolicy.json"))));

	        services.AddScoped<IAuthorisationRestriction, UserAuthorisationRestriction>();
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

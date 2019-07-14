using ApplicationManager.ApplicationServices.Authentication;
using ApplicationManager.ApplicationServices.Authentication.ClientCredential;
using ApplicationManager.ApplicationServices.AuthenticationServices;
using ApplicationManager.ApplicationServices.Identities;
using ApplicationManager.ApplicationServices.Identities.Claims;
using ApplicationManager.ApplicationServices.Identities.CreateAdmin;
using ApplicationManager.ApplicationServices.Identities.TwoFactor;
using ApplicationManager.ApplicationServices.Notifications.Audiences;
using ApplicationManager.ApplicationServices.Notifications.Emails;
using ApplicationManager.ApplicationServices.Notifications.Send;
using ApplicationManager.ApplicationServices.Users.CreateAdmin;
using ApplicationManager.Domain.AuthenticationServices;
using ApplicationManager.Domain.AuthenticationServices.ChangeClientCredential;
using ApplicationManager.Domain.AuthenticationServices.CreateClientCredential;
using ApplicationManager.Domain.Identities;
using ApplicationManager.Domain.Identities.AddClaim;
using ApplicationManager.Domain.Identities.ChangeClaim;
using ApplicationManager.Domain.Identities.ChangePassword;
using ApplicationManager.Domain.Identities.CheckPassword;
using ApplicationManager.Domain.Identities.ConfirmIdentity;
using ApplicationManager.Domain.Identities.Create;
using ApplicationManager.Domain.Identities.CreateRefreshToken;
using ApplicationManager.Domain.Identities.ForgotPassword;
using ApplicationManager.Domain.Identities.Login.ClientCredential;
using ApplicationManager.Domain.Identities.Login.Password;
using ApplicationManager.Domain.Identities.Login.RefreshToken;
using ApplicationManager.Domain.Identities.Logout;
using ApplicationManager.Domain.Identities.Queries;
using ApplicationManager.Domain.Identities.RegisterClientCredential;
using ApplicationManager.Domain.Identities.RegisterPassword;
using ApplicationManager.Domain.Identities.ResendConfirmIdentity;
using ApplicationManager.Domain.Identities.ResetPassword;
using ApplicationManager.Domain.NotificationTypes;
using ApplicationManager.Domain.Users;
using ApplicationManager.Domain.Users.Create;
using ApplicationManager.Persistence;
using ApplicationManager.Persistence.Identities;
using ApplicationManager.Setup.Infrastructure.Authentication.ClientCredential;
using Common.Api.Routing;
using Common.ApplicationServices.Transactions;
using Common.Domain.Persistence;
using Common.Setup.Infrastructure.Authorisation;
using FluentValidation;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Diagnostics;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Persistence.EntityFramework.Infrastructure.Transactions;
using Persistence.EntityFramework.Repositories;

namespace ApplicationManager.Setup
{
    public static class ServiceCollectionExtensions
    {
        public static IServiceCollection RegisterApplicationServices(this IServiceCollection services)
        {
            return services
                .AddScoped<ICreateAdminAuthenticationIdentityKernalService, CreateAdminAuthenticationIdentityKernalService>()
                .AddScoped<ISendNotificationKernalService, SendNotificationKernalService>()
                .AddScoped<ISendTwoFactorAuthenticationNotificationKernalService, SendTwoFactorAuthenticationNotificationKernalService>()
                .AddScoped<ICreateAdminUserApplicationKernalService, CreateAdminUserApplicationKernalService>()
                .AddScoped<IIdentityClaimsApplicationKernalService, IdentityClaimsApplicationKernalService>()
                .RegisterApplicationService<IIdentityApplicationService, IdentityApplicationService, IdentityApplicationServiceSecurityDecorator>()
                .RegisterApplicationService<IAuthenticationApplicationService, AuthenticationApplicationService, AuthenticationApplicationServiceSecurityDecorator>()
                .RegisterApplicationService<IAuthenticationServiceApplicationService, AuthenticationServiceApplicationService, AuthenticationServiceApplicationServiceSecurityDecorator>()
                .AddScoped<IClientCredentialAuthenticationValidator, ClientCredentialAuthenticationValidator>();
        }

        public static IServiceCollection RegisterValidators(this IServiceCollection services)
        {
            ValidatorOptions.LanguageManager.Enabled = false;

            return services
                .AddScoped<IValidator<CreateUserCommandDdto>, CreateUserCommandValidator>()
                .AddScoped<IValidator<ResetPasswordCommandDdto>, ResetPasswordValidator>()
                .AddScoped<IValidator<PasswordLoginCommandDdto>, PasswordLoginCommandValidator>()
                .AddScoped<IValidator<CheckPasswordDdto>, CheckPasswordValidator>()
                .AddScoped<IValidator<ChangePasswordCommandDdto>, ChangePasswordValidator>()
                .AddScoped<IValidator<RegisterPasswordCommandDdto>, RegisterPasswordValidator>()
                .AddScoped<IValidator<ConfirmIdentityCommandDdto>, ConfirmIdentityValidator>()
                .AddScoped<IValidator<ForgotPasswordCommandDdto>, ForgotPasswordCommandValidator>()
                .AddScoped<IValidator<RefreshTokenLoginCommandDdto>, RefreshTokenLoginCommandValidator>()
                .AddScoped<IValidator<RegisterClientCredentialCommandDdto>, RegisterClientCredentialCommandValidator>()
                .AddScoped<IValidator<CreateAuthenticationGrantTypeClientCredentialDdto>, CreateAuthenticationGrantTypeClientCredentialValidator>()
                .AddScoped<IValidator<ChangeAuthenticationGrantTypeClientCredentialDdto>, ChangeAuthenticationGrantTypeClientCredentialValidator>()
                .AddScoped<IValidator<AddIdentityClaimCommandDdto>, AddIdentityClaimValidator>()
                .AddScoped<IValidator<ChangeIdentityClaimCommandDdto>, ChangeIdentityClaimValidator>();
        }

        public static IServiceCollection RegisterDomainServices(this IServiceCollection services)
        {
            return services
                .AddScoped<ICreateIdentityCommand, CreateIdentityCommand>()
                .AddScoped<ICreateUserCommand, CreateUserCommand>()
                .AddScoped<ICreateUserCommand, CreateUserCommand>()
                .AddScoped<IResetPasswordCommand, ResetPasswordCommand>()
                .AddScoped<IPasswordLoginCommand, PasswordLoginCommand>()
                .AddScoped<ICheckPasswordCommand, CheckPasswordCommand>()
                .AddScoped<IChangePasswordCommand, ChangePasswordCommand>()
                .AddScoped<IRegisterPasswordCommand, RegisterPasswordCommand>()
                .AddScoped<IConfirmIdentityCommand, ConfirmIdentityCommand>()
                .AddScoped<IForgotPasswordCommand, ForgotPasswordCommand>()
                .AddScoped<ICreateRefreshTokenCommand, CreateRefreshTokenCommand>()
                .AddScoped<ILogoutCommand, LogoutCommand>()
                .AddScoped<IResendConfirmIdentityCommand, ResendConfirmIdentityCommand>()
                .AddScoped<IRefreshTokenLoginCommand, RefreshTokenLoginCommand>()
                .AddScoped<IRegisterClientCredentialCommand, RegisterClientCredentialCommand>()
                .AddScoped<IClientCredentialLoginCommand, ClientCredentialLoginCommand>()
                .AddScoped<ICreateAuthenticationGrantTypeClientCredentialCommand, CreateAuthenticationGrantTypeClientCredentialCommand>()
                .AddScoped<IChangeAuthenticationGrantTypeClientCredentialCommand, ChangeAuthenticationGrantTypeClientCredentialCommand>()
                .AddScoped<IAddIdentityClaimCommand, AddIdentityClaimCommand>()
                .AddScoped<IChangeIdentityClaimCommand, ChangeIdentityClaimCommand>();
        }

        public static IServiceCollection RegisterPersistenceServices(
            this IServiceCollection services,
            IConfiguration configuration)
        {
            services.AddScoped<IGetTwoFactorAuthenticationIdentityByIdentityQuery, GetTwoFactorAuthenticationIdentityByIdentityQuery>();
            services.AddScoped<IPasswordIdentityIdentifierExistsQuery, PasswordIdentityIdentifierExistsQuery>();
            services.AddScoped<IGetIdentityByIdentifierAndPasswordQuery, GetIdentityByIdentifierAndPasswordQuery>();
            services.AddScoped<IGetIdentityByEmailAddressQuery, GetIdentityByEmailAddressQuery>();
            services.AddScoped<IGetIdentityByForgotPasswordTokenQuery, GetIdentityByForgotPasswordTokenQuery>();
            services.AddScoped<IGetIdentityBySessionQuery, GetIdentityBySessionQuery>();
            services.AddScoped<IGetIdentityByClientCredentialIdentifierQuery, GetIdentityByClientCredentialIdentifierQuery>();
            services.AddScoped<IPasswordIdentityEmailExistsQuery, PasswordIdentityEmailExistsQuery>();

            services.AddScoped<ICommandRepository<AuthenticationService>, CommandRepository<AuthenticationService>>();
            services.AddScoped<ICommandRepository<Identity>, IdentityCommandRepository>();
            services.AddScoped<ICommandRepository<User>, CommandRepository<User>>();

            services.AddScoped<IQueryRepository<AuthenticationService>, QueryRepository<AuthenticationService>>();
            services.AddScoped<IQueryRepository<Identity>, QueryRepository<Identity>>();
            services.AddScoped<IQueryRepository<NotificationType>, QueryRepository<NotificationType>>();
            services.AddScoped<IQueryRepository<User>, QueryRepository<User>>();

            services.AddEntityFrameworkSqlServer().AddOptions()
                .AddDbContext<ApplicationManagerDbContext>(options =>
                    options.UseLazyLoadingProxies()
                        .ConfigureWarnings(warnings => warnings.Throw(RelationalEventId.QueryClientEvaluationWarning))
                        .UseSqlServer(configuration.GetConnectionString("Default")));
            services.AddScoped<DbContext>(sp => sp.GetRequiredService<ApplicationManagerDbContext>());
            services.AddScoped<ITransactionManager, EntityFrameworkTransactionManager>();

            return services;
        }

        public static IServiceCollection RegisterProviders(this IServiceCollection services)
        {
            return services
                .AddSingleton<IRouteProvider<bool>, DefaultRouteProvider>()
                .AddSingleton<IAbsoluteRouteProvider, DefaultAbsoluteRouteProvider>();
        }

        public static IServiceCollection UseEmailNotifications(this IServiceCollection services)
        {
            return services.AddScoped<IEmailBuilder, EmailBuilder>()
                .AddScoped<IChannelAudienceResolverProvider, ChannelAudienceResolverProvider>()
                .AddScoped<IChannelAudienceResolver, TwoFactorAuthenticationEmailChannelResolver>()
                .AddScoped<IEmailNotificationSender, LocalDevelopmentEmailNotificationSender>();
        }
    }
}
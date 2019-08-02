using Authentication.ApplicationServices.Authentication;
using Authentication.ApplicationServices.Authentication.ClientCredential;
using Authentication.ApplicationServices.AuthenticationServices;
using Authentication.ApplicationServices.Identities;
using Authentication.ApplicationServices.Identities.Claims;
using Authentication.ApplicationServices.Identities.CreateAdmin;
using Authentication.ApplicationServices.Identities.TwoFactor;
using Authentication.ApplicationServices.Notifications.Audiences;
using Authentication.ApplicationServices.Notifications.Emails;
using Authentication.ApplicationServices.Notifications.Send;
using Authentication.ApplicationServices.Users.CreateAdmin;
using Authentication.Domain.AuthenticationServices;
using Authentication.Domain.AuthenticationServices.ChangeFacebook;
using Authentication.Domain.AuthenticationServices.ChangeGoogle;
using Authentication.Domain.AuthenticationServices.CreateFacebook;
using Authentication.Domain.AuthenticationServices.CreateGoogle;
using Authentication.Domain.Identities;
using Authentication.Domain.Identities.AddOrChangeClaim;
using Authentication.Domain.Identities.ChangePassword;
using Authentication.Domain.Identities.CheckPassword;
using Authentication.Domain.Identities.ConfirmIdentity;
using Authentication.Domain.Identities.Create;
using Authentication.Domain.Identities.CreateRefreshToken;
using Authentication.Domain.Identities.ForgotPassword;
using Authentication.Domain.Identities.Login.ClientCredential;
using Authentication.Domain.Identities.Login.Password;
using Authentication.Domain.Identities.Login.RefreshToken;
using Authentication.Domain.Identities.Logout;
using Authentication.Domain.Identities.Queries;
using Authentication.Domain.Identities.RegisterClientCredential;
using Authentication.Domain.Identities.RegisterPassword;
using Authentication.Domain.Identities.ResendConfirmIdentity;
using Authentication.Domain.Identities.ResetPassword;
using Authentication.Domain.NotificationTypes;
using Authentication.Domain.Users;
using Authentication.Domain.Users.Create;
using Authentication.Setup.Infrastructure.Authorisation;
using Authentication.Persistence;
using Authentication.Persistence.Identities;
using Authentication.Setup.Infrastructure.Authentication.Facebook;
using Authentication.Setup.Infrastructure.Authentication.Google;
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
using ReferenceData.Setup;

namespace Authentication.Setup
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
                .AddSecureApplicationService<IIdentityApplicationService, IdentityApplicationService, IdentityApplicationServiceSecurityDecorator>()
                .AddSecureApplicationService<IAuthenticationApplicationService, AuthenticationApplicationService, AuthenticationApplicationServiceSecurityDecorator>()
                .AddSecureApplicationService<IFacebookAuthenticationServiceApplicationService, FacebookAuthenticationServiceApplicationService, FacebookAuthenticationServiceApplicationServiceSecurityDecorator>()
                .AddSecureApplicationService<IGoogleAuthenticationServiceApplicationService, GoogleAuthenticationServiceApplicationService, GoogleAuthenticationServiceApplicationServiceSecurityDecorator>()
                .AddSecureApplicationService<IAuthenticationServiceApplicationService, AuthenticationServiceApplicationService, AuthenticationServiceApplicationServiceSecurityDecorator>()
                .AddScoped<IFacebookAuthenticationValidator, FacebookAuthenticationValidator>()
                .AddScoped<IGoogleAuthenticationValidator, GoogleAuthenticationValidator>();
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
                .AddScoped<IValidator<CreateAuthenticationGrantTypeFacebookDdto>, CreateAuthenticationGrantTypeFacebookValidator>()
                .AddScoped<IValidator<ChangeAuthenticationGrantTypeFacebookDdto>, ChangeAuthenticationGrantTypeFacebookValidator>()
                .AddScoped<IValidator<CreateAuthenticationGrantTypeGoogleDdto>, CreateAuthenticationGrantTypeGoogleValidator>()
                .AddScoped<IValidator<ChangeAuthenticationGrantTypeGoogleDdto>, ChangeAuthenticationGrantTypeGoogleValidator>()
                .AddScoped<IValidator<AddOrChangeIdentityClaimCommandDdto>, AddOrChangeIdentityClaimValidator>();
        }

        public static IServiceCollection RegisterDomainCommands(this IServiceCollection services)
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
                .AddScoped<ICreateAuthenticationGrantTypeFacebookCommand, CreateAuthenticationGrantTypeFacebookCommand>()
                .AddScoped<IChangeAuthenticationGrantTypeFacebookCommand, ChangeAuthenticationGrantTypeFacebookCommand>()
                .AddScoped<ICreateAuthenticationGrantTypeGoogleCommand, CreateAuthenticationGrantTypeGoogleCommand>()
                .AddScoped<IChangeAuthenticationGrantTypeGoogleCommand, ChangeAuthenticationGrantTypeGoogleCommand>()
                .AddScoped<IAddOrChangeIdentityClaimCommand, AddOrChangeIdentityClaimCommand>();
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
                .AddDbContext<AuthenticationDbContext>(options =>
                    options.UseLazyLoadingProxies()
                        .ConfigureWarnings(warnings => warnings.Throw(RelationalEventId.QueryClientEvaluationWarning))
                        .UseSqlServer(configuration.GetConnectionString("Default")));
            services.AddScoped<DbContext>(sp => sp.GetRequiredService<AuthenticationDbContext>());
            services.AddScoped<ITransactionManager, EntityFrameworkTransactionManager>();

            services.AddReferenceData<AuthenticationDbContext>();

            return services;
        }

        public static IServiceCollection RegisterProviders(this IServiceCollection services)
        {
            return services
                .AddSingleton<ICurrentUserProvider, CurrentUserProvider>()
                .AddSingleton<IRouteProvider<bool>, DefaultRouteProvider>()
                .AddSingleton<IAbsoluteRouteProvider, DefaultAbsoluteRouteProvider>();
        }

        public static IServiceCollection UseEmailNotifications(this IServiceCollection services)
        {
            return services.AddScoped<IEmailBuilder, EmailBuilder>()
                .AddScoped<IChannelAudienceResolverProvider, ChannelAudienceResolverProvider>()
                .AddScoped<IChannelAudienceResolver, TwoFactorAuthenticationEmailChannelResolver>();
        }
    }
}
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
        public static IServiceCollection AddAuthenticationServiceDomain(this IServiceCollection services)
        {
            return services
                .AddSecureApplicationService<IAuthenticationApplicationService, AuthenticationApplicationService, AuthenticationApplicationServiceSecurityDecorator>()
                .AddSecureApplicationService<IFacebookAuthenticationServiceApplicationService, FacebookAuthenticationServiceApplicationService,
                    FacebookAuthenticationServiceApplicationServiceSecurityDecorator>()
                .AddSecureApplicationService<IGoogleAuthenticationServiceApplicationService, GoogleAuthenticationServiceApplicationService,
                    GoogleAuthenticationServiceApplicationServiceSecurityDecorator>()
                .AddSecureApplicationService<IAuthenticationServiceApplicationService, AuthenticationServiceApplicationService,
                    AuthenticationServiceApplicationServiceSecurityDecorator>()
                .AddScoped<IFacebookAuthenticationValidator, FacebookAuthenticationValidator>()
                .AddScoped<IGoogleAuthenticationValidator, GoogleAuthenticationValidator>()
                .AddScoped<IValidator<PasswordLoginCommandDdto>, PasswordLoginCommandValidator>()
                .AddScoped<IValidator<RefreshTokenLoginCommandDdto>, RefreshTokenLoginCommandValidator>()
                .AddScoped<IValidator<RegisterClientCredentialCommandDdto>, RegisterClientCredentialCommandValidator>()
                .AddScoped<IValidator<CreateAuthenticationGrantTypeFacebookDdto>, CreateAuthenticationGrantTypeFacebookValidator>()
                .AddScoped<IValidator<ChangeAuthenticationGrantTypeFacebookDdto>, ChangeAuthenticationGrantTypeFacebookValidator>()
                .AddScoped<IValidator<CreateAuthenticationGrantTypeGoogleDdto>, CreateAuthenticationGrantTypeGoogleValidator>()
                .AddScoped<IValidator<ChangeAuthenticationGrantTypeGoogleDdto>, ChangeAuthenticationGrantTypeGoogleValidator>()
                .AddScoped<IRefreshTokenLoginCommand, RefreshTokenLoginCommand>()
                .AddScoped<IRegisterClientCredentialCommand, RegisterClientCredentialCommand>()
                .AddScoped<IClientCredentialLoginCommand, ClientCredentialLoginCommand>()
                .AddScoped<ICreateAuthenticationGrantTypeFacebookCommand, CreateAuthenticationGrantTypeFacebookCommand>()
                .AddScoped<IChangeAuthenticationGrantTypeFacebookCommand, ChangeAuthenticationGrantTypeFacebookCommand>()
                .AddScoped<ICreateAuthenticationGrantTypeGoogleCommand, CreateAuthenticationGrantTypeGoogleCommand>()
                .AddScoped<IChangeAuthenticationGrantTypeGoogleCommand, ChangeAuthenticationGrantTypeGoogleCommand>()
                .AddScoped<IQueryRepository<AuthenticationService>, QueryRepository<AuthenticationService>>()
                .AddScoped<ICommandRepository<AuthenticationService>, CommandRepository<AuthenticationService>>();
        }

        public static IServiceCollection AddUserDomain(this IServiceCollection services)
        {
            return services
                .AddScoped<ICreateUserApplicationKernalService, CreateUserApplicationKernalService>()
                .AddScoped<IValidator<CreateUserCommandDdto>, CreateUserCommandValidator>()
                .AddScoped<ICreateUserCommand, CreateUserCommand>()
                .AddScoped<ICommandRepository<User>, CommandRepository<User>>()
                .AddScoped<IQueryRepository<User>, QueryRepository<User>>()
                .AddSingleton<ICurrentUserProvider, CurrentUserProvider>();
        }

        public static IServiceCollection AddNotificationDomain(this IServiceCollection services)
        {
            return services
                .AddScoped<ISendNotificationKernalService, SendNotificationKernalService>()
                .AddScoped<ISendTwoFactorAuthenticationNotificationKernalService, SendTwoFactorAuthenticationNotificationKernalService>()
                .AddScoped<IQueryRepository<NotificationType>, QueryRepository<NotificationType>>();
        }

        public static IServiceCollection AddIdentityDomain(this IServiceCollection services)
        {
            return services
                .AddScoped<ICreateAdminAuthenticationIdentityKernalService, CreateAdminAuthenticationIdentityKernalService>()
                .AddScoped<IIdentityClaimsApplicationKernalService, IdentityClaimsApplicationKernalService>()
                .AddSecureApplicationService<IIdentityApplicationService, IdentityApplicationService, IdentityApplicationServiceSecurityDecorator>()
                .AddScoped<IValidator<ResetPasswordCommandDdto>, ResetPasswordValidator>()
                .AddScoped<IValidator<CheckPasswordDdto>, CheckPasswordValidator>()
                .AddScoped<IValidator<ChangePasswordCommandDdto>, ChangePasswordValidator>()
                .AddScoped<IValidator<RegisterPasswordCommandDdto>, RegisterPasswordValidator>()
                .AddScoped<IValidator<ConfirmIdentityCommandDdto>, ConfirmIdentityValidator>()
                .AddScoped<IValidator<ForgotPasswordCommandDdto>, ForgotPasswordCommandValidator>()
                .AddScoped<IValidator<AddOrChangeIdentityClaimCommandDdto>, AddOrChangeIdentityClaimValidator>()
                .AddScoped<ICreateIdentityCommand, CreateIdentityCommand>()
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
                .AddScoped<IAddOrChangeIdentityClaimCommand, AddOrChangeIdentityClaimCommand>()
                .AddScoped<IGetTwoFactorAuthenticationIdentityByIdentityQuery, GetTwoFactorAuthenticationIdentityByIdentityQuery>()
                .AddScoped<IPasswordIdentityIdentifierExistsQuery, PasswordIdentityIdentifierExistsQuery>()
                .AddScoped<IGetIdentityByIdentifierAndPasswordQuery, GetIdentityByIdentifierAndPasswordQuery>()
                .AddScoped<IGetIdentityByEmailAddressQuery, GetIdentityByEmailAddressQuery>()
                .AddScoped<IGetIdentityByForgotPasswordTokenQuery, GetIdentityByForgotPasswordTokenQuery>()
                .AddScoped<IGetIdentityBySessionQuery, GetIdentityBySessionQuery>()
                .AddScoped<IGetIdentityByClientCredentialIdentifierQuery, GetIdentityByClientCredentialIdentifierQuery>()
                .AddScoped<IPasswordIdentityEmailExistsQuery, PasswordIdentityEmailExistsQuery>()
                .AddScoped<ICommandRepository<Identity>, IdentityCommandRepository>()
                .AddScoped<IQueryRepository<Identity>, QueryRepository<Identity>>();
        }

        public static IServiceCollection AddAuthenticationPersistence(
            this IServiceCollection services,
            IConfiguration configuration)
        {
            return services.AddEntityFrameworkSqlServer().AddOptions()
                .AddDbContext<AuthenticationDbContext>(options =>
                    options.UseLazyLoadingProxies()
                        .ConfigureWarnings(warnings => warnings.Throw(RelationalEventId.QueryClientEvaluationWarning))
                        .UseSqlServer(configuration.GetConnectionString("Default")))
                .AddScoped<DbContext>(sp => sp.GetRequiredService<AuthenticationDbContext>())
                .AddScoped<ITransactionManager, EntityFrameworkTransactionManager>()
                .AddReferenceData<AuthenticationDbContext>();
        }

        public static IServiceCollection UseDefaultRouting(this IServiceCollection services)
        {
            return services
                .AddSingleton<IRouteProvider<bool>, DefaultRouteProvider>();
        }

        public static IServiceCollection UseEmailNotifications(this IServiceCollection services)
        {
            return services.AddScoped<IEmailBuilder, EmailBuilder>()
                .AddScoped<IChannelAudienceResolverProvider, ChannelAudienceResolverProvider>()
                .AddScoped<IChannelAudienceResolver, TwoFactorAuthenticationEmailChannelResolver>();
        }
    }
}
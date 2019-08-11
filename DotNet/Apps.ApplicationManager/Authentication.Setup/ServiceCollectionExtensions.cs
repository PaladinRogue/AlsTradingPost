using FluentValidation;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Diagnostics;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using PaladinRogue.Authentication.Application.Authentication;
using PaladinRogue.Authentication.Application.Authentication.ClientCredential;
using PaladinRogue.Authentication.Application.AuthenticationServices;
using PaladinRogue.Authentication.Application.Identities;
using PaladinRogue.Authentication.Application.Identities.Claims;
using PaladinRogue.Authentication.Application.Identities.CreateAdmin;
using PaladinRogue.Authentication.Application.Identities.TwoFactor;
using PaladinRogue.Authentication.Application.Notifications.Audiences;
using PaladinRogue.Authentication.Application.Notifications.Emails;
using PaladinRogue.Authentication.Application.Notifications.Send;
using PaladinRogue.Authentication.Application.Users.CreateAdmin;
using PaladinRogue.Authentication.Domain.AuthenticationServices;
using PaladinRogue.Authentication.Domain.AuthenticationServices.ChangeFacebook;
using PaladinRogue.Authentication.Domain.AuthenticationServices.ChangeGoogle;
using PaladinRogue.Authentication.Domain.AuthenticationServices.CreateFacebook;
using PaladinRogue.Authentication.Domain.AuthenticationServices.CreateGoogle;
using PaladinRogue.Authentication.Domain.Identities;
using PaladinRogue.Authentication.Domain.Identities.AddOrChangeClaim;
using PaladinRogue.Authentication.Domain.Identities.ChangePassword;
using PaladinRogue.Authentication.Domain.Identities.CheckPassword;
using PaladinRogue.Authentication.Domain.Identities.ConfirmIdentity;
using PaladinRogue.Authentication.Domain.Identities.Create;
using PaladinRogue.Authentication.Domain.Identities.CreateRefreshToken;
using PaladinRogue.Authentication.Domain.Identities.ForgotPassword;
using PaladinRogue.Authentication.Domain.Identities.Login.ClientCredential;
using PaladinRogue.Authentication.Domain.Identities.Login.Password;
using PaladinRogue.Authentication.Domain.Identities.Login.RefreshToken;
using PaladinRogue.Authentication.Domain.Identities.Logout;
using PaladinRogue.Authentication.Domain.Identities.Queries;
using PaladinRogue.Authentication.Domain.Identities.RegisterClientCredential;
using PaladinRogue.Authentication.Domain.Identities.RegisterPassword;
using PaladinRogue.Authentication.Domain.Identities.ResendConfirmIdentity;
using PaladinRogue.Authentication.Domain.Identities.ResetPassword;
using PaladinRogue.Authentication.Domain.NotificationTypes;
using PaladinRogue.Authentication.Domain.Users;
using PaladinRogue.Authentication.Domain.Users.Create;
using PaladinRogue.Authentication.Persistence;
using PaladinRogue.Authentication.Persistence.Identities;
using PaladinRogue.Authentication.Setup.Infrastructure.Authentication.Facebook;
using PaladinRogue.Authentication.Setup.Infrastructure.Authentication.Google;
using PaladinRogue.Authentication.Setup.Infrastructure.Authorisation;
using PaladinRogue.Libray.Authorisation.Setup.Infrastructure.Authorisation;
using PaladinRogue.Libray.Core.Application.Transactions;
using PaladinRogue.Libray.Core.Domain.Persistence;
using PaladinRogue.Libray.Core.Setup.Infrastructure.Routing;
using PaladinRogue.Libray.Persistence.EntityFramework.Repositories;
using PaladinRogue.Libray.Persistence.Setup.Infrastructure.Transactions;
using PaladinRogue.Libray.ReferenceData.Setup;

namespace PaladinRogue.Authentication.Setup
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
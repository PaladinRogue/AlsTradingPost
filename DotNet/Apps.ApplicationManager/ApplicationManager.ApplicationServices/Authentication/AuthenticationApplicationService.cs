using System;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;
using ApplicationManager.ApplicationServices.Authentication.ClientCredential;
using ApplicationManager.ApplicationServices.Authentication.Models;
using ApplicationManager.Domain.AuthenticationServices;
using ApplicationManager.Domain.Identities;
using ApplicationManager.Domain.Identities.Login;
using ApplicationManager.Domain.Identities.Login.ClientCredential;
using ApplicationManager.Domain.Identities.Login.Password;
using ApplicationManager.Domain.Identities.Login.RefreshToken;
using ApplicationManager.Domain.Users;
using Common.ApplicationServices;
using Common.ApplicationServices.Authentication;
using Common.ApplicationServices.Exceptions;
using Common.ApplicationServices.Transactions;
using Common.Domain.Exceptions;
using Common.Domain.Persistence;
using ClaimsBuilder = ApplicationManager.ApplicationServices.Claims.ClaimsBuilder;
using JwtClaims = Common.ApplicationServices.Authentication.Constants.JwtClaims;

namespace ApplicationManager.ApplicationServices.Authentication
{
    public class AuthenticationApplicationService : IAuthenticationApplicationService
    {
        private readonly ICommandRepository<User> _userCommandRepository;

        private readonly IPasswordLoginCommand _passwordLoginCommand;

        private readonly IRefreshTokenLoginCommand _refreshTokenLoginCommand;

        private readonly IJwtFactory _jwtFactory;

        private readonly ITransactionManager _transactionManager;

        private readonly ICommandRepository<Identity> _identityCommandRepository;

        private readonly IQueryRepository<AuthenticationService> _authenticationServiceCommandRepository;

        private readonly IClientCredentialAuthenticationValidator _clientCredentialAuthenticationValidator;

        private readonly IClientCredentialLoginCommand _clientCredentialLoginCommand;

        public AuthenticationApplicationService(
            ICommandRepository<User> userCommandRepository,
            IPasswordLoginCommand passwordLoginCommand,
            IJwtFactory jwtFactory,
            ITransactionManager transactionManager,
            ICommandRepository<Identity> identityCommandRepository,
            IRefreshTokenLoginCommand refreshTokenLoginCommand,
            IQueryRepository<AuthenticationService> authenticationServiceCommandRepository,
            IClientCredentialAuthenticationValidator clientCredentialAuthenticationValidator,
            IClientCredentialLoginCommand clientCredentialLoginCommand)
        {
            _userCommandRepository = userCommandRepository;
            _passwordLoginCommand = passwordLoginCommand;
            _jwtFactory = jwtFactory;
            _transactionManager = transactionManager;
            _identityCommandRepository = identityCommandRepository;
            _refreshTokenLoginCommand = refreshTokenLoginCommand;
            _authenticationServiceCommandRepository = authenticationServiceCommandRepository;
            _clientCredentialAuthenticationValidator = clientCredentialAuthenticationValidator;
            _clientCredentialLoginCommand = clientCredentialLoginCommand;
        }

        public async Task<JwtAdto> Password(PasswordAdto passwordAdto)
        {
            using (ITransaction transaction = _transactionManager.Create())
            {
                try
                {
                    if (!(_authenticationServiceCommandRepository.GetSingle(s => s is AuthenticationGrantTypePassword) is AuthenticationGrantTypePassword))
                    {
                        throw new BusinessApplicationException(ExceptionType.BadRequest, ErrorCodes.PasswordLoginNotConfigured, "Password logins are not configured");
                    }

                    Identity identity = _passwordLoginCommand.Execute(new PasswordLoginCommandDdto
                    {
                        Identifier = passwordAdto.Identifier,
                        Password = passwordAdto.Password
                    });

                    _identityCommandRepository.Update(identity);

                    transaction.Commit();

                    return await _jwtFactory.GenerateJwt<JwtAdto>(CreateClaimsIdentity(identity), identity.Session.Id);
                }
                catch (InvalidLoginDomainException)
                {
                    throw new BusinessApplicationException(ExceptionType.BadRequest, ErrorCodes.InvalidLogin, "Your login details are incorrect");
                }
                catch (DomainValidationRuleException e)
                {
                    throw new BusinessValidationRuleApplicationException(e.ValidationResult);
                }
            }
        }

        public async Task<JwtAdto> RefreshToken(RefreshTokenAdto refreshTokenAdto)
        {
            using (ITransaction transaction = _transactionManager.Create())
            {
                try
                {
                    if (!(_authenticationServiceCommandRepository.GetSingle(s => s is AuthenticationGrantTypeRefreshToken) is AuthenticationGrantTypeRefreshToken))
                    {
                        throw new BusinessApplicationException(ExceptionType.BadRequest, ErrorCodes.RefreshTokenLoginNotConfigured, "Refresh token logins are not configured");
                    }

                    Identity identity = _refreshTokenLoginCommand.Execute(new RefreshTokenLoginCommandDdto
                    {
                        SessionId = refreshTokenAdto.SessionId,
                        RefreshToken = refreshTokenAdto.Token
                    });

                    _identityCommandRepository.Update(identity);

                    transaction.Commit();

                    return await _jwtFactory.GenerateJwt<JwtAdto>(CreateClaimsIdentity(identity), identity.Session.Id);
                }
                catch (InvalidLoginDomainException)
                {
                    throw new BusinessApplicationException(ExceptionType.Unauthorized, "Your login details are incorrect");
                }
                catch (DomainValidationRuleException e)
                {
                    throw new BusinessValidationRuleApplicationException(e.ValidationResult);
                }
            }
        }

        public async Task<JwtAdto> ClientCredential(ClientCredentialAdto clientCredentialAdto)
        {
            using (ITransaction transaction = _transactionManager.Create())
            {
                try
                {
                    Guid.TryParse(clientCredentialAdto.State, out Guid authenticationServiceId);

                    AuthenticationGrantTypeClientCredential authenticationGrantTypeClientCredential =
                        (AuthenticationGrantTypeClientCredential) _authenticationServiceCommandRepository.GetSingle(
                            s => s is AuthenticationGrantTypeClientCredential && s.Id == authenticationServiceId);

                    if (authenticationGrantTypeClientCredential == null)
                    {
                        throw new BusinessApplicationException(ExceptionType.BadRequest, ErrorCodes.ClientCredentialLoginNotConfigured, "Client credential login not configured");
                    }

                    IClientCredentialAuthenticationResult clientCredentialAuthenticationResult = await _clientCredentialAuthenticationValidator.Validate(authenticationGrantTypeClientCredential, new ValidateClientCredentialAdto
                    {
                        Token = clientCredentialAdto.Token,
                        RedirectUri = clientCredentialAdto.RedirectUri
                    });

                    if (!clientCredentialAuthenticationResult.Success)
                    {
                        throw new BusinessApplicationException(ExceptionType.Unauthorized, "Could not validate access token");
                    }

                    Identity identity = _clientCredentialLoginCommand.Execute(authenticationGrantTypeClientCredential, new ClientCredentialLoginCommandDdto
                    {
                        Identifier = clientCredentialAuthenticationResult.Identifier
                    });

                    _identityCommandRepository.Add(identity);

                    transaction.Commit();

                    return await _jwtFactory.GenerateJwt<JwtAdto>(CreateClaimsIdentity(identity), identity.Session.Id);
                }
                catch (InvalidLoginDomainException)
                {
                    throw new BusinessApplicationException(ExceptionType.Unauthorized, "Your login details are incorrect");
                }
                catch (DomainValidationRuleException e)
                {
                    throw new BusinessValidationRuleApplicationException(e.ValidationResult);
                }
            }
        }

        private ClaimsIdentity CreateClaimsIdentity(Identity identity)
        {
            User user = _userCommandRepository.GetSingle(u => u.Identity.Id == identity.Id);

            ClaimsBuilder claimsBuilder = ClaimsBuilder.CreateBuilder();

            if (user != null)
            {
                claimsBuilder.WithUser(user.Id);
            }

            claimsBuilder.WithSubject(identity.Id);

            if (identity.AuthenticationIdentities.Any(i => i is TwoFactorAuthenticationIdentity authenticationIdentity
                                                           && authenticationIdentity.TwoFactorAuthenticationType == TwoFactorAuthenticationType.ConfirmIdentity))
            {
                claimsBuilder.WithRole(JwtClaims.RestrictedAppAccess);
            }
            else
            {
                claimsBuilder.WithRole(JwtClaims.AppAccess);
            }

            ClaimsIdentity claimsIdentity = claimsBuilder.Build();
            return claimsIdentity;
        }
    }
}
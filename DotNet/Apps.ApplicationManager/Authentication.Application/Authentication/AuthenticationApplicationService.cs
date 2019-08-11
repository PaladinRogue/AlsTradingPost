using System.Security.Claims;
using System.Threading.Tasks;
using PaladinRogue.Authentication.Application.Authentication.ClientCredential;
using PaladinRogue.Authentication.Application.Authentication.Models;
using PaladinRogue.Authentication.Domain.AuthenticationServices;
using PaladinRogue.Authentication.Domain.Identities;
using PaladinRogue.Authentication.Domain.Identities.Create;
using PaladinRogue.Authentication.Domain.Identities.Login;
using PaladinRogue.Authentication.Domain.Identities.Login.ClientCredential;
using PaladinRogue.Authentication.Domain.Identities.Login.Password;
using PaladinRogue.Authentication.Domain.Identities.Login.RefreshToken;
using PaladinRogue.Authentication.Domain.Identities.Queries;
using PaladinRogue.Authentication.Domain.Identities.RegisterClientCredential;
using PaladinRogue.Libray.Core.Application;
using PaladinRogue.Libray.Core.Application.Authentication;
using PaladinRogue.Libray.Core.Application.Claims;
using PaladinRogue.Libray.Core.Application.Exceptions;
using PaladinRogue.Libray.Core.Application.Transactions;
using PaladinRogue.Libray.Core.Domain.Exceptions;
using PaladinRogue.Libray.Core.Domain.Persistence;
using Claim = PaladinRogue.Authentication.Domain.Identities.Claim;
using JwtClaims = PaladinRogue.Libray.Core.Application.Authentication.Constants.JwtClaims;

namespace PaladinRogue.Authentication.Application.Authentication
{
    public class AuthenticationApplicationService : IAuthenticationApplicationService
    {
        private readonly IPasswordLoginCommand _passwordLoginCommand;

        private readonly IRefreshTokenLoginCommand _refreshTokenLoginCommand;

        private readonly IJwtFactory _jwtFactory;

        private readonly ITransactionManager _transactionManager;

        private readonly ICommandRepository<Identity> _identityCommandRepository;

        private readonly IQueryRepository<AuthenticationService> _authenticationServiceCommandRepository;

        private readonly IGoogleAuthenticationValidator _googleAuthenticationValidator;

        private readonly IFacebookAuthenticationValidator _facebookAuthenticationValidator;

        private readonly IGetIdentityByClientCredentialIdentifierQuery _getIdentityByClientCredentialIdentifierQuery;

        private readonly ICreateIdentityCommand _createIdentityCommand;

        private readonly IClientCredentialLoginCommand _clientCredentialLoginCommand;

        private readonly IRegisterClientCredentialCommand _registerClientCredentialCommand;

        public AuthenticationApplicationService(
            IPasswordLoginCommand passwordLoginCommand,
            IJwtFactory jwtFactory,
            ITransactionManager transactionManager,
            ICommandRepository<Identity> identityCommandRepository,
            IRefreshTokenLoginCommand refreshTokenLoginCommand,
            IQueryRepository<AuthenticationService> authenticationServiceCommandRepository,
            IGoogleAuthenticationValidator googleAuthenticationValidator,
            IFacebookAuthenticationValidator facebookAuthenticationValidator,
            IClientCredentialLoginCommand clientCredentialLoginCommand,
            ICreateIdentityCommand createIdentityCommand,
            IGetIdentityByClientCredentialIdentifierQuery getIdentityByClientCredentialIdentifierQuery,
            IRegisterClientCredentialCommand registerClientCredentialCommand)
        {
            _passwordLoginCommand = passwordLoginCommand;
            _jwtFactory = jwtFactory;
            _transactionManager = transactionManager;
            _identityCommandRepository = identityCommandRepository;
            _refreshTokenLoginCommand = refreshTokenLoginCommand;
            _authenticationServiceCommandRepository = authenticationServiceCommandRepository;
            _googleAuthenticationValidator = googleAuthenticationValidator;
            _facebookAuthenticationValidator = facebookAuthenticationValidator;
            _clientCredentialLoginCommand = clientCredentialLoginCommand;
            _createIdentityCommand = createIdentityCommand;
            _getIdentityByClientCredentialIdentifierQuery = getIdentityByClientCredentialIdentifierQuery;
            _registerClientCredentialCommand = registerClientCredentialCommand;
        }

        public async Task<JwtAdto> PasswordAsync(PasswordAdto passwordAdto)
        {
            using (ITransaction transaction = _transactionManager.Create())
            {
                try
                {
                    if (!(await _authenticationServiceCommandRepository.GetSingleAsync(s => s is AuthenticationGrantTypePassword) is AuthenticationGrantTypePassword))
                    {
                        throw new BusinessApplicationException(ExceptionType.BadRequest, ErrorCodes.PasswordLoginNotConfigured, "Password logins are not configured");
                    }

                    Identity identity = await _passwordLoginCommand.ExecuteAsync(new PasswordLoginCommandDdto
                    {
                        Identifier = passwordAdto.Identifier,
                        Password = passwordAdto.Password
                    });

                    await _identityCommandRepository.UpdateAsync(identity);

                    JwtAdto jwtAdto = await _jwtFactory.GenerateJwtAsync<JwtAdto>(GetClaimsIdentity(identity), identity.Session.Id);

                    transaction.Commit();

                    return jwtAdto;
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

        public async Task<JwtAdto> RefreshTokenAsync(RefreshTokenAdto refreshTokenAdto)
        {
            using (ITransaction transaction = _transactionManager.Create())
            {
                try
                {
                    if (!(await _authenticationServiceCommandRepository.GetSingleAsync(s => s is AuthenticationGrantTypeRefreshToken) is AuthenticationGrantTypeRefreshToken))
                    {
                        throw new BusinessApplicationException(ExceptionType.BadRequest, ErrorCodes.RefreshTokenLoginNotConfigured, "Refresh token logins are not configured");
                    }

                    Identity identity = await _refreshTokenLoginCommand.ExecuteAsync(new RefreshTokenLoginCommandDdto
                    {
                        SessionId = refreshTokenAdto.SessionId,
                        RefreshToken = refreshTokenAdto.Token
                    });

                    await _identityCommandRepository.UpdateAsync(identity);

                    JwtAdto jwtAdto = await _jwtFactory.GenerateJwtAsync<JwtAdto>(GetClaimsIdentity(identity), identity.Session.Id);

                    transaction.Commit();

                    return jwtAdto;
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

        public async Task<JwtAdto> GoogleAsync(ClientCredentialAdto clientCredentialAdto)
        {
            using (ITransaction transaction = _transactionManager.Create())
            {
                try
                {
                    AuthenticationGrantTypeGoogle authenticationGrantTypeGoogle =
                        (AuthenticationGrantTypeGoogle) await _authenticationServiceCommandRepository.GetSingleAsync(
                            s => s is AuthenticationGrantTypeGoogle && s.Id == clientCredentialAdto.Id);

                    if (authenticationGrantTypeGoogle == null)
                    {
                        throw new BusinessApplicationException(ExceptionType.BadRequest, ErrorCodes.ClientCredentialLoginNotConfigured, "Client credential login not configured");
                    }

                    IClientCredentialAuthenticationResult clientCredentialAuthenticationResult = await _googleAuthenticationValidator.Validate(authenticationGrantTypeGoogle, new ValidateClientCredentialAdto
                    {
                        Token = clientCredentialAdto.Token,
                        RedirectUri = clientCredentialAdto.RedirectUri
                    });

                    if (!clientCredentialAuthenticationResult.Success)
                    {
                        throw new BusinessApplicationException(ExceptionType.Unauthorized, "Could not validate access token");
                    }

                    Identity identity = await _getIdentityByClientCredentialIdentifierQuery.RunAsync(authenticationGrantTypeGoogle, clientCredentialAuthenticationResult.Identifier);

                    if (identity == null)
                    {
                        identity = await _createIdentityCommand.ExecuteAsync();

                        await _registerClientCredentialCommand.ExecuteAsync(identity, authenticationGrantTypeGoogle, new RegisterClientCredentialCommandDdto
                        {
                            Identifier = clientCredentialAuthenticationResult.Identifier
                        });

                        await _identityCommandRepository.AddAsync(identity);
                    }

                    await _clientCredentialLoginCommand.ExecuteAsync(identity);

                    await _identityCommandRepository.UpdateAsync(identity);

                    JwtAdto jwtAdto = await _jwtFactory.GenerateJwtAsync<JwtAdto>(GetClaimsIdentity(identity), identity.Session.Id);

                    transaction.Commit();

                    return jwtAdto;
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

        public async Task<JwtAdto> FacebookAsync(ClientCredentialAdto clientCredentialAdto)
        {
            using (ITransaction transaction = _transactionManager.Create())
            {
                try
                {
                    AuthenticationGrantTypeFacebook authenticationGrantTypeFacebook =
                        (AuthenticationGrantTypeFacebook) await _authenticationServiceCommandRepository.GetSingleAsync(
                            s => s is AuthenticationGrantTypeFacebook && s.Id == clientCredentialAdto.Id);

                    if (authenticationGrantTypeFacebook == null)
                    {
                        throw new BusinessApplicationException(ExceptionType.BadRequest, ErrorCodes.ClientCredentialLoginNotConfigured, "Client credential login not configured");
                    }

                    IClientCredentialAuthenticationResult clientCredentialAuthenticationResult = await _facebookAuthenticationValidator.Validate(authenticationGrantTypeFacebook, new ValidateClientCredentialAdto
                    {
                        Token = clientCredentialAdto.Token,
                        RedirectUri = clientCredentialAdto.RedirectUri
                    });

                    if (!clientCredentialAuthenticationResult.Success)
                    {
                        throw new BusinessApplicationException(ExceptionType.Unauthorized, "Could not validate access token");
                    }

                    Identity identity = await _getIdentityByClientCredentialIdentifierQuery.RunAsync(authenticationGrantTypeFacebook, clientCredentialAuthenticationResult.Identifier);

                    if (identity == null)
                    {
                        identity = await _createIdentityCommand.ExecuteAsync();

                        await _registerClientCredentialCommand.ExecuteAsync(identity, authenticationGrantTypeFacebook, new RegisterClientCredentialCommandDdto
                        {
                            Identifier = clientCredentialAuthenticationResult.Identifier
                        });

                        await _identityCommandRepository.AddAsync(identity);
                    }

                    await _clientCredentialLoginCommand.ExecuteAsync(identity);

                    await _identityCommandRepository.UpdateAsync(identity);

                    JwtAdto jwtAdto = await _jwtFactory.GenerateJwtAsync<JwtAdto>(GetClaimsIdentity(identity), identity.Session.Id);

                    transaction.Commit();

                    return jwtAdto;
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

        private ClaimsIdentity GetClaimsIdentity(Identity identity)
        {
            ClaimsBuilder claimsBuilder = ClaimsBuilder.CreateBuilder();

            foreach (Claim identityClaim in identity.Claims)
            {
                claimsBuilder.AddClaim(identityClaim.Type, identityClaim.Value);
            }

            claimsBuilder.WithSubject(identity.Id);

            claimsBuilder.WithRole(identity.IsConfirmed ? JwtClaims.AppAccess : JwtClaims.RestrictedAppAccess);

            ClaimsIdentity claimsIdentity = claimsBuilder.Build();

            return claimsIdentity;
        }
    }
}
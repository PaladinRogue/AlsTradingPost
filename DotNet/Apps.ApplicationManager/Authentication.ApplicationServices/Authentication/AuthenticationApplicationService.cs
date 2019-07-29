using System.Security.Claims;
using System.Threading.Tasks;
using Authentication.ApplicationServices.Authentication.ClientCredential;
using Authentication.ApplicationServices.Authentication.Models;
using Authentication.Domain.AuthenticationServices;
using Authentication.Domain.Identities;
using Authentication.Domain.Identities.Create;
using Authentication.Domain.Identities.Login;
using Authentication.Domain.Identities.Login.ClientCredential;
using Authentication.Domain.Identities.Login.Password;
using Authentication.Domain.Identities.Login.RefreshToken;
using Authentication.Domain.Identities.Queries;
using Authentication.Domain.Identities.RegisterClientCredential;
using Common.ApplicationServices;
using Common.ApplicationServices.Authentication;
using Common.ApplicationServices.Claims;
using Common.ApplicationServices.Exceptions;
using Common.ApplicationServices.Transactions;
using Common.Domain.Exceptions;
using Common.Domain.Persistence;
using Claim = Authentication.Domain.Identities.Claim;
using JwtClaims = Common.ApplicationServices.Authentication.Constants.JwtClaims;

namespace Authentication.ApplicationServices.Authentication
{
    public class AuthenticationApplicationService : IAuthenticationApplicationService
    {
        private readonly IPasswordLoginCommand _passwordLoginCommand;

        private readonly IRefreshTokenLoginCommand _refreshTokenLoginCommand;

        private readonly IJwtFactory _jwtFactory;

        private readonly ITransactionManager _transactionManager;

        private readonly ICommandRepository<Identity> _identityCommandRepository;

        private readonly IQueryRepository<AuthenticationService> _authenticationServiceCommandRepository;

        private readonly IClientCredentialAuthenticationValidator _clientCredentialAuthenticationValidator;

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
            IClientCredentialAuthenticationValidator clientCredentialAuthenticationValidator,
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
            _clientCredentialAuthenticationValidator = clientCredentialAuthenticationValidator;
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

        public async Task<JwtAdto> ClientCredentialAsync(ClientCredentialAdto clientCredentialAdto)
        {
            using (ITransaction transaction = _transactionManager.Create())
            {
                try
                {
                    AuthenticationGrantTypeClientCredential authenticationGrantTypeClientCredential =
                        (AuthenticationGrantTypeClientCredential) await _authenticationServiceCommandRepository.GetSingleAsync(
                            s => s is AuthenticationGrantTypeClientCredential && s.Id == clientCredentialAdto.Id);

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

                    Identity identity = await _getIdentityByClientCredentialIdentifierQuery.RunAsync(authenticationGrantTypeClientCredential, clientCredentialAuthenticationResult.Identifier);

                    if (identity == null)
                    {
                        identity = await _createIdentityCommand.ExecuteAsync();

                        await _registerClientCredentialCommand.ExecuteAsync(identity, authenticationGrantTypeClientCredential, new RegisterClientCredentialCommandDdto
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
using ApplicationManager.ApplicationServices.AuthenticationServices.Models;
using ApplicationManager.Domain.AuthenticationServices;
using ApplicationManager.Domain.AuthenticationServices.CreateClientCredential;
using Common.ApplicationServices.Concurrency;
using Common.ApplicationServices.Exceptions;
using Common.ApplicationServices.Transactions;
using Common.Domain.Exceptions;
using Common.Domain.Persistence;

namespace ApplicationManager.ApplicationServices.AuthenticationServices
{
    public class AuthenticationServiceApplicationService : IAuthenticationServiceApplicationService
    {
        private readonly ICommandRepository<AuthenticationService> _commandRepository;

        private readonly ITransactionManager _transactionManager;

        private readonly ICreateAuthenticationGrantTypeClientCredentialCommand _createAuthenticationGrantTypeClientCredentialCommand;

        public AuthenticationServiceApplicationService(
            ICommandRepository<AuthenticationService> commandRepository,
            ITransactionManager transactionManager,
            ICreateAuthenticationGrantTypeClientCredentialCommand createAuthenticationGrantTypeClientCredentialCommand)
        {
            _commandRepository = commandRepository;
            _transactionManager = transactionManager;
            _createAuthenticationGrantTypeClientCredentialCommand = createAuthenticationGrantTypeClientCredentialCommand;
        }

        public ClientCredentialAdto CreateClientCredential(CreateClientCredentialAdto createClientCredentialAdto)
        {
            using (ITransaction transaction = _transactionManager.Create())
            {
                try
                {
                    AuthenticationGrantTypeClientCredential authenticationGrantTypeClientCredential = _createAuthenticationGrantTypeClientCredentialCommand.Execute(new CreateAuthenticationGrantTypeClientCredentialDdto
                    {
                        Name = createClientCredentialAdto.Name,
                        ClientId = createClientCredentialAdto.ClientId,
                        ClientSecret = createClientCredentialAdto.ClientSecret,
                        ClientGrantAccessTokenUrl = createClientCredentialAdto.ClientGrantAccessTokenUrl,
                        GrantAccessTokenUrl = createClientCredentialAdto.GrantAccessTokenUrl,
                        ValidateAccessTokenUrl = createClientCredentialAdto.ValidateAccessTokenUrl
                    });

                    _commandRepository.Add(authenticationGrantTypeClientCredential);

                    transaction.Commit();

                    return new ClientCredentialAdto
                    {
                        Id = authenticationGrantTypeClientCredential.Id,
                        Name = authenticationGrantTypeClientCredential.Name,
                        ClientId = authenticationGrantTypeClientCredential.MaskedClientId,
                        ClientSecret = authenticationGrantTypeClientCredential.MaskedClientSecret,
                        ClientGrantAccessTokenUrl = authenticationGrantTypeClientCredential.ClientGrantAccessTokenUrl,
                        GrantAccessTokenUrl = authenticationGrantTypeClientCredential.GrantAccessTokenUrl,
                        ValidateAccessTokenUrl = authenticationGrantTypeClientCredential.ValidateAccessTokenUrl,
                        Version = ConcurrencyVersionFactory.CreateFromEntity(authenticationGrantTypeClientCredential)
                    };
                }
                catch (DomainValidationRuleException e)
                {
                    throw new BusinessValidationRuleApplicationException(e.ValidationResult);
                }
            }
        }
    }
}
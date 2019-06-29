using ApplicationManager.ApplicationServices.AuthenticationServices.Models;
using ApplicationManager.Domain.AuthenticationServices;
using ApplicationManager.Domain.AuthenticationServices.CreateClientCredential;
using AutoMapper;
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

        private readonly IQueryRepository<AuthenticationService> _queryRepository;

        private readonly ITransactionManager _transactionManager;

        private readonly ICreateAuthenticationGrantTypeClientCredentialCommand _createAuthenticationGrantTypeClientCredentialCommand;

        private readonly IMapper _mapper;

        public AuthenticationServiceApplicationService(
            ICommandRepository<AuthenticationService> commandRepository,
            IQueryRepository<AuthenticationService> queryRepository,
            ITransactionManager transactionManager,
            ICreateAuthenticationGrantTypeClientCredentialCommand createAuthenticationGrantTypeClientCredentialCommand,
            IMapper mapper)
        {
            _commandRepository = commandRepository;
            _transactionManager = transactionManager;
            _createAuthenticationGrantTypeClientCredentialCommand = createAuthenticationGrantTypeClientCredentialCommand;
            _mapper = mapper;
            _queryRepository = queryRepository;
        }

        public ClientCredentialAdto CreateClientCredential(CreateClientCredentialAdto createClientCredentialAdto)
        {
            using (ITransaction transaction = _transactionManager.Create())
            {
                try
                {
                    AuthenticationGrantTypeClientCredential authenticationGrantTypeClientCredential =
                        _createAuthenticationGrantTypeClientCredentialCommand.Execute(
                            _mapper.Map<CreateClientCredentialAdto, CreateAuthenticationGrantTypeClientCredentialDdto>(createClientCredentialAdto));

                    _commandRepository.Add(authenticationGrantTypeClientCredential);

                    transaction.Commit();

                    return CreateClientCredentialAdto(authenticationGrantTypeClientCredential);
                }
                catch (DomainValidationRuleException e)
                {
                    throw new BusinessValidationRuleApplicationException(e.ValidationResult);
                }
            }
        }

        public ClientCredentialAdto GetClientCredential(GetClientCredentialAdto getClientCredentialAdto)
        {
            using (ITransaction transaction = _transactionManager.Create())
            {
                if (!(_queryRepository.GetById(getClientCredentialAdto.Id) is AuthenticationGrantTypeClientCredential authenticationGrantTypeClientCredential))
                {
                    throw new BusinessApplicationException(ExceptionType.NotFound, "Authentication service not found");
                }

                transaction.Commit();

                return CreateClientCredentialAdto(authenticationGrantTypeClientCredential);
            }
        }

        private static ClientCredentialAdto CreateClientCredentialAdto(AuthenticationGrantTypeClientCredential authenticationGrantTypeClientCredential)
        {
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
    }
}
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using ApplicationManager.ApplicationServices.AuthenticationServices.Models;
using ApplicationManager.Domain.AuthenticationServices;
using ApplicationManager.Domain.AuthenticationServices.ChangeClientCredential;
using ApplicationManager.Domain.AuthenticationServices.CreateClientCredential;
using AutoMapper;
using Common.ApplicationServices.Concurrency;
using Common.ApplicationServices.Exceptions;
using Common.ApplicationServices.Transactions;
using Common.Domain.Exceptions;
using Common.Domain.Persistence;
using Common.Resources.Builders.Dictionaries;
using Common.Resources.Extensions;

namespace ApplicationManager.ApplicationServices.AuthenticationServices
{
    public class AuthenticationServiceApplicationService : IAuthenticationServiceApplicationService
    {
        private readonly ICommandRepository<AuthenticationService> _commandRepository;

        private readonly IQueryRepository<AuthenticationService> _queryRepository;

        private readonly ITransactionManager _transactionManager;

        private readonly ICreateAuthenticationGrantTypeClientCredentialCommand _createAuthenticationGrantTypeClientCredentialCommand;

        private readonly IChangeAuthenticationGrantTypeClientCredentialCommand _changeAuthenticationGrantTypeClientCredentialCommand;

        private readonly IMapper _mapper;

        public AuthenticationServiceApplicationService(
            ICommandRepository<AuthenticationService> commandRepository,
            IQueryRepository<AuthenticationService> queryRepository,
            ITransactionManager transactionManager,
            ICreateAuthenticationGrantTypeClientCredentialCommand createAuthenticationGrantTypeClientCredentialCommand,
            IChangeAuthenticationGrantTypeClientCredentialCommand changeAuthenticationGrantTypeClientCredentialCommand,
            IMapper mapper)
        {
            _commandRepository = commandRepository;
            _transactionManager = transactionManager;
            _createAuthenticationGrantTypeClientCredentialCommand = createAuthenticationGrantTypeClientCredentialCommand;
            _mapper = mapper;
            _changeAuthenticationGrantTypeClientCredentialCommand = changeAuthenticationGrantTypeClientCredentialCommand;
            _queryRepository = queryRepository;
        }

        public async Task<IEnumerable<AuthenticationServiceAdto>> GetAuthenticationServicesAsync()
        {
            using (ITransaction transaction = _transactionManager.Create())
            {
                IQueryable<AuthenticationService> authenticationServices = await _queryRepository.GetAsync();

                IList<AuthenticationServiceAdto> authenticationServiceAdtos = new List<AuthenticationServiceAdto>();

                foreach (AuthenticationService authenticationService in authenticationServices)
                {
                    switch (authenticationService)
                    {
                        case AuthenticationGrantTypeClientCredential authenticationGrantTypeClientCredential:
                            authenticationServiceAdtos.Add(new ClientCredentialAuthenticationServiceAdto
                            {
                                Id = authenticationGrantTypeClientCredential.Id,
                                Type = authenticationGrantTypeClientCredential.Name,
                                AccessUrl = BuildClientAccessUrl(authenticationGrantTypeClientCredential)
                            });
                            break;
                        // ReSharper disable once UnusedVariable - Because of switch case parameter is mandatory
                        case AuthenticationGrantTypePassword authenticationGrantTypePassword:
                            authenticationServiceAdtos.Add(new PasswordAuthenticationServiceAdto
                            {
                                Type = "Password"
                            });
                            break;
                    }
                }

                transaction.Commit();

                return authenticationServiceAdtos;
            }
        }

        public async Task<ClientCredentialAdto> CreateClientCredential(CreateClientCredentialAdto createClientCredentialAdto)
        {
            using (ITransaction transaction = _transactionManager.Create())
            {
                try
                {
                    AuthenticationGrantTypeClientCredential authenticationGrantTypeClientCredential =
                        await _createAuthenticationGrantTypeClientCredentialCommand.ExecuteAsync(
                            _mapper.Map<CreateClientCredentialAdto, CreateAuthenticationGrantTypeClientCredentialDdto>(createClientCredentialAdto));

                    await _commandRepository.AddAsync(authenticationGrantTypeClientCredential);

                    transaction.Commit();

                    return CreateClientCredentialAdto(authenticationGrantTypeClientCredential);
                }
                catch (DomainValidationRuleException e)
                {
                    throw new BusinessValidationRuleApplicationException(e.ValidationResult);
                }
            }
        }

        public async Task<ClientCredentialAdto> GetClientCredentialAsync(GetClientCredentialAdto getClientCredentialAdto)
        {
            using (ITransaction transaction = _transactionManager.Create())
            {
                if (!(await _queryRepository.GetByIdAsync(getClientCredentialAdto.Id) is AuthenticationGrantTypeClientCredential authenticationGrantTypeClientCredential))
                {
                    throw new BusinessApplicationException(ExceptionType.NotFound, "Authentication service not found");
                }

                transaction.Commit();

                return CreateClientCredentialAdto(authenticationGrantTypeClientCredential);
            }
        }

        public async Task<ClientCredentialAdto> ChangeClientCredentialAsync(ChangeClientCredentialAdto changeClientCredentialAdto)
        {
            using (ITransaction transaction = _transactionManager.Create())
            {
                try
                {
                    if (!(await _commandRepository.GetWithConcurrencyCheckAsync(changeClientCredentialAdto.Id, changeClientCredentialAdto.Version) is AuthenticationGrantTypeClientCredential
                        authenticationGrantTypeClientCredential))
                    {
                        throw new BusinessApplicationException(ExceptionType.NotFound, "Authentication service not found");
                    }

                    await _changeAuthenticationGrantTypeClientCredentialCommand.ExecuteAsync(authenticationGrantTypeClientCredential,
                        _mapper.Map<ChangeClientCredentialAdto, ChangeAuthenticationGrantTypeClientCredentialDdto>(changeClientCredentialAdto));

                    await _commandRepository.UpdateAsync(authenticationGrantTypeClientCredential);

                    transaction.Commit();

                    return CreateClientCredentialAdto(authenticationGrantTypeClientCredential);
                }
                catch (DomainValidationRuleException e)
                {
                    throw new BusinessValidationRuleApplicationException(e.ValidationResult);
                }
                catch (ConcurrencyDomainException e)
                {
                    throw new BusinessApplicationException(ExceptionType.Concurrency, e);
                }
                catch (NotFoundDomainException e)
                {
                    throw new BusinessApplicationException(ExceptionType.NotFound, e);
                }
            }
        }

        private string BuildClientAccessUrl(AuthenticationGrantTypeClientCredential authenticationGrantTypeClientCredential)
        {
            return authenticationGrantTypeClientCredential.ClientGrantAccessTokenUrl.Format(
                DictionaryBuilder<string, object>.Create()
                    .Add("clientId", authenticationGrantTypeClientCredential.ClientId)
                    .Build());
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
                AppAccessToken = authenticationGrantTypeClientCredential.MaskedAppAccessToken,
                Version = ConcurrencyVersionFactory.CreateFromEntity(authenticationGrantTypeClientCredential)
            };
        }
    }
}
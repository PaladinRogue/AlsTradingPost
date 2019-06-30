using System.Collections.Generic;
using System.Linq;
using ApplicationManager.ApplicationServices.AuthenticationServices.Models;
using ApplicationManager.Domain.AuthenticationServices;
using ApplicationManager.Domain.AuthenticationServices.ChangeClientCredential;
using ApplicationManager.Domain.AuthenticationServices.CreateClientCredential;
using AutoMapper;
using Common.Api.Routing;
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

        private readonly IChangeAuthenticationGrantTypeClientCredentialCommand _changeAuthenticationGrantTypeClientCredentialCommand;

        private readonly IMapper _mapper;

        private readonly IAbsoluteRouteProvider _absoluteRouteProvider;

        public AuthenticationServiceApplicationService(
            ICommandRepository<AuthenticationService> commandRepository,
            IQueryRepository<AuthenticationService> queryRepository,
            ITransactionManager transactionManager,
            ICreateAuthenticationGrantTypeClientCredentialCommand createAuthenticationGrantTypeClientCredentialCommand,
            IChangeAuthenticationGrantTypeClientCredentialCommand changeAuthenticationGrantTypeClientCredentialCommand,
            IMapper mapper,
            IAbsoluteRouteProvider absoluteRouteProvider)
        {
            _commandRepository = commandRepository;
            _transactionManager = transactionManager;
            _createAuthenticationGrantTypeClientCredentialCommand = createAuthenticationGrantTypeClientCredentialCommand;
            _mapper = mapper;
            _changeAuthenticationGrantTypeClientCredentialCommand = changeAuthenticationGrantTypeClientCredentialCommand;
            _absoluteRouteProvider = absoluteRouteProvider;
            _queryRepository = queryRepository;
        }

        public IEnumerable<AuthenticationServiceAdto> GetAuthenticationServices()
        {
            using (ITransaction transaction = _transactionManager.Create())
            {
                IQueryable<AuthenticationService> authenticationServices = _queryRepository.Get();

                IList<AuthenticationServiceAdto> authenticationServiceAdtos = new List<AuthenticationServiceAdto>();

                foreach (AuthenticationService authenticationService in authenticationServices)
                {
                    switch (authenticationService)
                    {
                        case AuthenticationGrantTypeClientCredential authenticationGrantTypeClientCredential:
                            authenticationServiceAdtos.Add(new AuthenticationServiceAdto
                            {
                                Type = authenticationGrantTypeClientCredential.Name,
                                AccessUrl = BuildAccessUrl(authenticationGrantTypeClientCredential)
                            });
                            break;
                        case AuthenticationGrantTypePassword authenticationGrantTypePassword:
                            authenticationServiceAdtos.Add(new AuthenticationServiceAdto
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

        public ClientCredentialAdto ChangeClientCredential(ChangeClientCredentialAdto changeClientCredentialAdto)
        {
            using (ITransaction transaction = _transactionManager.Create())
            {
                try
                {
                    if (!(_queryRepository.GetWithConcurrencyCheck(changeClientCredentialAdto.Id, changeClientCredentialAdto.Version) is AuthenticationGrantTypeClientCredential
                        authenticationGrantTypeClientCredential))
                    {
                        throw new BusinessApplicationException(ExceptionType.NotFound, "Authentication service not found");
                    }

                    _changeAuthenticationGrantTypeClientCredentialCommand.Execute(authenticationGrantTypeClientCredential,
                        _mapper.Map<ChangeClientCredentialAdto, ChangeAuthenticationGrantTypeClientCredentialDdto>(changeClientCredentialAdto));

                    _commandRepository.Update(authenticationGrantTypeClientCredential);

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

        private string BuildAccessUrl(AuthenticationGrantTypeClientCredential authenticationGrantTypeClientCredential)
        {
            string redirect = _absoluteRouteProvider.GetRouteTemplate(RouteDictionary.AuthenticateClientCredential, new {});

            return string.Format(
                authenticationGrantTypeClientCredential.ClientGrantAccessTokenUrl,
                authenticationGrantTypeClientCredential.ClientId,
                redirect,
                authenticationGrantTypeClientCredential.Id);
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
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using PaladinRogue.Authentication.Application.AuthenticationServices.Models;
using PaladinRogue.Authentication.Application.AuthenticationServices.Models.Facebook;
using PaladinRogue.Authentication.Application.AuthenticationServices.Models.Google;
using PaladinRogue.Authentication.Domain;
using PaladinRogue.Authentication.Domain.AuthenticationServices;
using PaladinRogue.Libray.Core.Application.Exceptions;
using PaladinRogue.Libray.Core.Application.Transactions;
using PaladinRogue.Libray.Core.Common.Builders.Dictionaries;
using PaladinRogue.Libray.Core.Common.Extensions;
using PaladinRogue.Libray.Core.Domain.Exceptions;
using PaladinRogue.Libray.Core.Domain.Persistence;
using PaladinRogue.Libray.ReferenceData.Domain.Persistence;
using PaladinRogue.Libray.ReferenceData.Domain.Projections;

namespace PaladinRogue.Authentication.Application.AuthenticationServices
{
    public class AuthenticationServiceApplicationService : IAuthenticationServiceApplicationService
    {
        private readonly ICommandRepository<AuthenticationService> _commandRepository;

        private readonly IQueryRepository<AuthenticationService> _queryRepository;

        private readonly IReferenceDataQueryRepository _referenceDataQueryRepository;

        private readonly ITransactionManager _transactionManager;

        private readonly IMapper _mapper;

        public AuthenticationServiceApplicationService(
            ICommandRepository<AuthenticationService> commandRepository,
            IQueryRepository<AuthenticationService> queryRepository,
            IReferenceDataQueryRepository referenceDataQueryRepository,
            ITransactionManager transactionManager,
            IMapper mapper)
        {
            _commandRepository = commandRepository;
            _queryRepository = queryRepository;
            _referenceDataQueryRepository = referenceDataQueryRepository;
            _transactionManager = transactionManager;
            _mapper = mapper;
        }

        public async Task<IEnumerable<AuthenticationServiceAdto>> GetAuthenticationServicesAsync(GetAuthenticationServicesAdto getAuthenticationServicesAdto)
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
                            authenticationServiceAdtos.Add(_mapper.Map<AuthenticationGrantTypeClientCredential, ClientCredentialAuthenticationServiceAdto>(authenticationGrantTypeClientCredential,
                                opts => opts.AfterMap((src, dest) => dest.AccessUrl = BuildClientAccessUrl(src, getAuthenticationServicesAdto))));
                            break;
                        default:
                            authenticationServiceAdtos.Add(_mapper.Map<AuthenticationService, AuthenticationServiceAdto>(authenticationService));
                            break;
                    }
                }

                transaction.Commit();

                return authenticationServiceAdtos;
            }
        }

        public async Task<IEnumerable<AuthenticationServiceTypeAdto>> GetAuthenticationServiceTypes()
        {
            using (ITransaction transaction = _transactionManager.Create())
            {
                IEnumerable<ReferenceDataValueProjection> referenceDataValueProjections = await _referenceDataQueryRepository.GetAllAsync(ReferenceDataTypes.ClientCredentialAuthenticationGrantType);

                IList<AuthenticationServiceTypeAdto> authenticationServiceTypeAdtos = new List<AuthenticationServiceTypeAdto>();

                foreach (ReferenceDataValueProjection referenceDataValueProjection in referenceDataValueProjections)
                {
                    switch (referenceDataValueProjection.Code)
                    {
                        case ClientCredentialAuthenticationGrantTypes.Facebook:
                            authenticationServiceTypeAdtos.Add(new FacebookAuthenticationServiceTypeAdto
                            {
                                Id = referenceDataValueProjection.Id,
                                Code = referenceDataValueProjection.Code
                            });
                            break;
                        case ClientCredentialAuthenticationGrantTypes.Google:
                            authenticationServiceTypeAdtos.Add(new GoogleAuthenticationServiceTypeAdto
                            {
                                Id = referenceDataValueProjection.Id,
                                Code = referenceDataValueProjection.Code
                            });
                            break;
                        default:
                            throw new ArgumentOutOfRangeException(nameof(referenceDataValueProjection.Code));
                    }
                }

                transaction.Commit();

                return authenticationServiceTypeAdtos;
            }
        }

        public async Task DeleteClientCredentialAsync(DeleteClientCredentialAdto deleteClientCredentialAdto)
        {
            using (ITransaction transaction = _transactionManager.Create())
            {
                try
                {
                    if (!(await _commandRepository.GetWithConcurrencyCheckAsync(deleteClientCredentialAdto.Id, deleteClientCredentialAdto.Version) is AuthenticationGrantTypeClientCredential
                        authenticationGrantTypeClientCredential))
                    {
                        throw new BusinessApplicationException(ExceptionType.NotFound, "Authentication service not found");
                    }

                    await _commandRepository.DeleteAsync(authenticationGrantTypeClientCredential.Id);

                    transaction.Commit();
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

        private static string BuildClientAccessUrl(
            AuthenticationGrantTypeClientCredential authenticationGrantTypeClientCredential,
            GetAuthenticationServicesAdto getAuthenticationServicesAdto)
        {
            IDictionaryBuilder<string, object> builder = DictionaryBuilder<string, object>.Create()
                .Add("clientId", authenticationGrantTypeClientCredential.ClientId);

            if (!string.IsNullOrWhiteSpace(getAuthenticationServicesAdto.State))
            {
                builder.Add("state", getAuthenticationServicesAdto.State);
            }

            if (!string.IsNullOrWhiteSpace(getAuthenticationServicesAdto.RedirectUri))
            {
                builder.Add("redirectUri", getAuthenticationServicesAdto.RedirectUri);
            }

            return authenticationGrantTypeClientCredential.ClientGrantAccessTokenUrl.Format(builder.Build());
        }
    }
}
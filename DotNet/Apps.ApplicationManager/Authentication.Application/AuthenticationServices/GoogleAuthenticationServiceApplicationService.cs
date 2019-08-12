using System.Threading.Tasks;
using AutoMapper;
using PaladinRogue.Authentication.Application.AuthenticationServices.Models.Google;
using PaladinRogue.Authentication.Domain.AuthenticationServices;
using PaladinRogue.Authentication.Domain.AuthenticationServices.ChangeGoogle;
using PaladinRogue.Authentication.Domain.AuthenticationServices.CreateGoogle;
using PaladinRogue.Library.Core.Application.Concurrency;
using PaladinRogue.Library.Core.Application.Exceptions;
using PaladinRogue.Library.Core.Application.Transactions;
using PaladinRogue.Library.Core.Domain.Exceptions;
using PaladinRogue.Library.Core.Domain.Persistence;

namespace PaladinRogue.Authentication.Application.AuthenticationServices
{
    public class GoogleAuthenticationServiceApplicationService : IGoogleAuthenticationServiceApplicationService
    {
        private readonly ICommandRepository<AuthenticationService> _commandRepository;

        private readonly IQueryRepository<AuthenticationService> _queryRepository;

        private readonly ITransactionManager _transactionManager;

        private readonly ICreateAuthenticationGrantTypeGoogleCommand _createAuthenticationGrantTypeGoogleCommand;

        private readonly IChangeAuthenticationGrantTypeGoogleCommand _changeAuthenticationGrantTypeGoogleCommand;

        private readonly IMapper _mapper;

        public GoogleAuthenticationServiceApplicationService(
            ICommandRepository<AuthenticationService> commandRepository,
            IQueryRepository<AuthenticationService> queryRepository,
            ITransactionManager transactionManager,
            ICreateAuthenticationGrantTypeGoogleCommand createAuthenticationGrantTypeGoogleCommand,
            IChangeAuthenticationGrantTypeGoogleCommand changeAuthenticationGrantTypeGoogleCommand,
            IMapper mapper)
        {
            _commandRepository = commandRepository;
            _queryRepository = queryRepository;
            _transactionManager = transactionManager;
            _createAuthenticationGrantTypeGoogleCommand = createAuthenticationGrantTypeGoogleCommand;
            _changeAuthenticationGrantTypeGoogleCommand = changeAuthenticationGrantTypeGoogleCommand;
            _mapper = mapper;
        }

        public async Task<GoogleAdto> CreateAsync(CreateGoogleAdto createGoogleAdto)
        {
            using (ITransaction transaction = _transactionManager.Create())
            {
                try
                {
                    AuthenticationGrantTypeGoogle authenticationGrantTypeGoogle =
                        await _createAuthenticationGrantTypeGoogleCommand.ExecuteAsync(
                            _mapper.Map<CreateGoogleAdto, CreateAuthenticationGrantTypeGoogleDdto>(createGoogleAdto));

                    await _commandRepository.AddAsync(authenticationGrantTypeGoogle);

                    transaction.Commit();

                    return CreateGoogleAdto(authenticationGrantTypeGoogle);
                }
                catch (DomainValidationRuleException e)
                {
                    throw new BusinessValidationRuleApplicationException(e.ValidationResult);
                }
            }
        }

        public async Task<GoogleAdto> GetAsync(GetGoogleAdto getGoogleAdto)
        {
            using (ITransaction transaction = _transactionManager.Create())
            {
                if (!(await _queryRepository.GetByIdAsync(getGoogleAdto.Id) is AuthenticationGrantTypeGoogle authenticationGrantTypeGoogle))
                {
                    throw new BusinessApplicationException(ExceptionType.NotFound, "Authentication service not found");
                }

                transaction.Commit();

                return CreateGoogleAdto(authenticationGrantTypeGoogle);
            }
        }

        public async Task<GoogleAdto> ChangeAsync(ChangeGoogleAdto changeGoogleAdto)
        {
            using (ITransaction transaction = _transactionManager.Create())
            {
                try
                {
                    if (!(await _commandRepository.GetWithConcurrencyCheckAsync(changeGoogleAdto.Id, changeGoogleAdto.Version) is AuthenticationGrantTypeGoogle
                        authenticationGrantTypeGoogle))
                    {
                        throw new BusinessApplicationException(ExceptionType.NotFound, "Authentication service not found");
                    }

                    await _changeAuthenticationGrantTypeGoogleCommand.ExecuteAsync(authenticationGrantTypeGoogle,
                        _mapper.Map<ChangeGoogleAdto, ChangeAuthenticationGrantTypeGoogleDdto>(changeGoogleAdto));

                    await _commandRepository.UpdateAsync(authenticationGrantTypeGoogle);

                    transaction.Commit();

                    return CreateGoogleAdto(authenticationGrantTypeGoogle);
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

        private static GoogleAdto CreateGoogleAdto(AuthenticationGrantTypeGoogle authenticationGrantTypeGoogle)
        {
            return new GoogleAdto
            {
                Id = authenticationGrantTypeGoogle.Id,
                Name = authenticationGrantTypeGoogle.Name,
                ClientId = authenticationGrantTypeGoogle.MaskedClientId,
                ClientSecret = authenticationGrantTypeGoogle.MaskedClientSecret,
                ClientGrantAccessTokenUrl = authenticationGrantTypeGoogle.ClientGrantAccessTokenUrl,
                GrantAccessTokenUrl = authenticationGrantTypeGoogle.GrantAccessTokenUrl,
                ValidateAccessTokenUrl = authenticationGrantTypeGoogle.ValidateAccessTokenUrl,
                Version = ConcurrencyVersionFactory.CreateFromEntity(authenticationGrantTypeGoogle)
            };
        }
    }
}
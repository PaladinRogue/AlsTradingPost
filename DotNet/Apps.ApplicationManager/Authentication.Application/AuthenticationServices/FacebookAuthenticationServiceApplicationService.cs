using System.Threading.Tasks;
using Authentication.Application.AuthenticationServices.Models.Facebook;
using Authentication.Domain.AuthenticationServices;
using Authentication.Domain.AuthenticationServices.ChangeFacebook;
using Authentication.Domain.AuthenticationServices.CreateFacebook;
using AutoMapper;
using Common.Application.Concurrency;
using Common.Application.Exceptions;
using Common.Application.Transactions;
using Common.Domain.Exceptions;
using Common.Domain.Persistence;

namespace Authentication.Application.AuthenticationServices
{
    public class FacebookAuthenticationServiceApplicationService : IFacebookAuthenticationServiceApplicationService
    {
        private readonly ICommandRepository<AuthenticationService> _commandRepository;

        private readonly IQueryRepository<AuthenticationService> _queryRepository;

        private readonly ITransactionManager _transactionManager;

        private readonly ICreateAuthenticationGrantTypeFacebookCommand _createAuthenticationGrantTypeFacebookCommand;

        private readonly IChangeAuthenticationGrantTypeFacebookCommand _changeAuthenticationGrantTypeFacebookCommand;

        private readonly IMapper _mapper;

        public FacebookAuthenticationServiceApplicationService(
            ICommandRepository<AuthenticationService> commandRepository,
            IQueryRepository<AuthenticationService> queryRepository,
            ITransactionManager transactionManager,
            ICreateAuthenticationGrantTypeFacebookCommand createAuthenticationGrantTypeFacebookCommand,
            IChangeAuthenticationGrantTypeFacebookCommand changeAuthenticationGrantTypeFacebookCommand,
            IMapper mapper)
        {
            _commandRepository = commandRepository;
            _queryRepository = queryRepository;
            _transactionManager = transactionManager;
            _createAuthenticationGrantTypeFacebookCommand = createAuthenticationGrantTypeFacebookCommand;
            _changeAuthenticationGrantTypeFacebookCommand = changeAuthenticationGrantTypeFacebookCommand;
            _mapper = mapper;
        }

        public async Task<FacebookAdto> CreateAsync(CreateFacebookAdto createFacebookAdto)
        {
            using (ITransaction transaction = _transactionManager.Create())
            {
                try
                {
                    AuthenticationGrantTypeFacebook authenticationGrantTypeFacebook =
                        await _createAuthenticationGrantTypeFacebookCommand.ExecuteAsync(
                            _mapper.Map<CreateFacebookAdto, CreateAuthenticationGrantTypeFacebookDdto>(createFacebookAdto));

                    await _commandRepository.AddAsync(authenticationGrantTypeFacebook);

                    transaction.Commit();

                    return CreateFacebookAdto(authenticationGrantTypeFacebook);
                }
                catch (DomainValidationRuleException e)
                {
                    throw new BusinessValidationRuleApplicationException(e.ValidationResult);
                }
            }
        }

        public async Task<FacebookAdto> GetAsync(GetFacebookAdto getFacebookAdto)
        {
            using (ITransaction transaction = _transactionManager.Create())
            {
                if (!(await _queryRepository.GetByIdAsync(getFacebookAdto.Id) is AuthenticationGrantTypeFacebook authenticationGrantTypeFacebook))
                {
                    throw new BusinessApplicationException(ExceptionType.NotFound, "Authentication service not found");
                }

                transaction.Commit();

                return CreateFacebookAdto(authenticationGrantTypeFacebook);
            }
        }

        public async Task<FacebookAdto> ChangeAsync(ChangeFacebookAdto changeFacebookAdto)
        {
            using (ITransaction transaction = _transactionManager.Create())
            {
                try
                {
                    if (!(await _commandRepository.GetWithConcurrencyCheckAsync(changeFacebookAdto.Id, changeFacebookAdto.Version) is AuthenticationGrantTypeFacebook
                        authenticationGrantTypeFacebook))
                    {
                        throw new BusinessApplicationException(ExceptionType.NotFound, "Authentication service not found");
                    }

                    await _changeAuthenticationGrantTypeFacebookCommand.ExecuteAsync(authenticationGrantTypeFacebook,
                        _mapper.Map<ChangeFacebookAdto, ChangeAuthenticationGrantTypeFacebookDdto>(changeFacebookAdto));

                    await _commandRepository.UpdateAsync(authenticationGrantTypeFacebook);

                    transaction.Commit();

                    return CreateFacebookAdto(authenticationGrantTypeFacebook);
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

        private static FacebookAdto CreateFacebookAdto(AuthenticationGrantTypeFacebook authenticationGrantTypeFacebook)
        {
            return new FacebookAdto
            {
                Id = authenticationGrantTypeFacebook.Id,
                Name = authenticationGrantTypeFacebook.Name,
                ClientId = authenticationGrantTypeFacebook.MaskedClientId,
                ClientSecret = authenticationGrantTypeFacebook.MaskedClientSecret,
                ClientGrantAccessTokenUrl = authenticationGrantTypeFacebook.ClientGrantAccessTokenUrl,
                GrantAccessTokenUrl = authenticationGrantTypeFacebook.GrantAccessTokenUrl,
                ValidateAccessTokenUrl = authenticationGrantTypeFacebook.ValidateAccessTokenUrl,
                AppAccessToken = authenticationGrantTypeFacebook.MaskedAppAccessToken,
                Version = ConcurrencyVersionFactory.CreateFromEntity(authenticationGrantTypeFacebook)
            };
        }
    }
}
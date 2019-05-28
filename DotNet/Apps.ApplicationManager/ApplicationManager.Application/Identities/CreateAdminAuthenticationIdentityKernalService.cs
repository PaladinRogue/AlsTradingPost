using ApplicationManager.ApplicationServices.Identities.Interfaces;
using ApplicationManager.ApplicationServices.Identities.Models;
using ApplicationManager.Domain.Identities;
using ApplicationManager.Domain.Identities.AuthenticationIdentities;
using AutoMapper;
using Common.Application.Exceptions;
using Common.Application.Transactions;
using Common.ApplicationServices.Services.Command;
using Common.Domain.Exceptions;

namespace ApplicationManager.ApplicationServices.Identities
{
    public class CreateAdminAuthenticationIdentityKernalService : ICreateAdminAuthenticationIdentityKernalService
    {
        private readonly ICommandService<Identity> _identityCommandService;

        private readonly ITransactionManager _transactionManager;

        private readonly ICreateTwoFactorAuthenticationIdentityCommand _createTwoFactorAuthenticationIdentityCommand;

        private readonly IMapper _mapper;

        public CreateAdminAuthenticationIdentityKernalService(
            ICommandService<Identity> identityCommandService,
            ITransactionManager transactionManager,
            ICreateTwoFactorAuthenticationIdentityCommand createTwoFactorAuthenticationIdentityCommand,
            IMapper mapper)
        {
            _identityCommandService = identityCommandService;
            _transactionManager = transactionManager;
            _createTwoFactorAuthenticationIdentityCommand = createTwoFactorAuthenticationIdentityCommand;
            _mapper = mapper;
        }

        public void Create(CreateAdminAuthenticationIdentityAdto createAdminAuthenticationIdentityAdto)
        {
            using (ITransaction transaction = _transactionManager.Create())
            {
                Identity identity = Identity.Create();
                _identityCommandService.Create(identity);

                try
                {
                    _createTwoFactorAuthenticationIdentityCommand.Execute(identity,
                        _mapper.Map<CreateAdminAuthenticationIdentityAdto, CreateTwoFactorAuthenticationIdentityDdto>(
                            createAdminAuthenticationIdentityAdto));
                }
                catch (DomainValidationRuleException e)
                {
                    throw new BusinessValidationRuleApplicationException(e.ValidationResult);
                }

                transaction.Commit();
            }
        }
    }
}


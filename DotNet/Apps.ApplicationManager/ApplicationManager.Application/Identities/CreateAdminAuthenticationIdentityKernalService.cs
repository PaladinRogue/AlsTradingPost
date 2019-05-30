using ApplicationManager.ApplicationServices.Identities.Interfaces;
using ApplicationManager.ApplicationServices.Identities.Models;
using ApplicationManager.Domain.Identities;
using ApplicationManager.Domain.Identities.AuthenticationIdentities;
using AutoMapper;
using Common.Application.Exceptions;
using Common.Application.Transactions;
using Common.Domain.Exceptions;
using Common.Domain.Persistence;

namespace ApplicationManager.ApplicationServices.Identities
{
    public class CreateAdminAuthenticationIdentityKernalService : ICreateAdminAuthenticationIdentityKernalService
    {
        private readonly ITransactionManager _transactionManager;

        private readonly ICreateIdentityCommand _createIdentityCommand;

        private readonly ICreateTwoFactorAuthenticationIdentityCommand _createTwoFactorAuthenticationIdentityCommand;

        private readonly ICommandRepository<Identity> _commandRepository;

        private readonly IMapper _mapper;

        public CreateAdminAuthenticationIdentityKernalService(
            ITransactionManager transactionManager,
            ICreateTwoFactorAuthenticationIdentityCommand createTwoFactorAuthenticationIdentityCommand,
            IMapper mapper,
            ICreateIdentityCommand createIdentityCommand,
            ICommandRepository<Identity> commandRepository)
        {
            _transactionManager = transactionManager;
            _createTwoFactorAuthenticationIdentityCommand = createTwoFactorAuthenticationIdentityCommand;
            _mapper = mapper;
            _createIdentityCommand = createIdentityCommand;
            _commandRepository = commandRepository;
        }

        public void Create(CreateAdminAuthenticationIdentityAdto createAdminAuthenticationIdentityAdto)
        {
            using (ITransaction transaction = _transactionManager.Create())
            {
                try
                {
                    Identity identity = _createIdentityCommand.Execute();

                    _commandRepository.Add(identity);

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


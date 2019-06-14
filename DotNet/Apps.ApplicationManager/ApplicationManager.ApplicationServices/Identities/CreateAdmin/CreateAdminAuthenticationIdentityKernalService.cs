using ApplicationManager.Domain.Identities;
using ApplicationManager.Domain.Identities.AddTwoFactor;
using ApplicationManager.Domain.Identities.Create;
using AutoMapper;
using Common.Application.Exceptions;
using Common.Application.Transactions;
using Common.Domain.Exceptions;
using Common.Domain.Persistence;
using Common.Messaging.Infrastructure;
using Common.Messaging.Messages;

namespace ApplicationManager.ApplicationServices.Identities.CreateAdmin
{
    public class CreateAdminAuthenticationIdentityKernalService : ICreateAdminAuthenticationIdentityKernalService
    {
        private readonly ITransactionManager _transactionManager;

        private readonly ICreateIdentityCommand _createIdentityCommand;

        private readonly IAddTwoFactorAuthenticationIdentityCommand _addTwoFactorAuthenticationIdentityCommand;

        private readonly ICommandRepository<Identity> _commandRepository;

        private readonly IMapper _mapper;

        public CreateAdminAuthenticationIdentityKernalService(
            ITransactionManager transactionManager,
            IAddTwoFactorAuthenticationIdentityCommand addTwoFactorAuthenticationIdentityCommand,
            IMapper mapper,
            ICreateIdentityCommand createIdentityCommand,
            ICommandRepository<Identity> commandRepository)
        {
            _transactionManager = transactionManager;
            _addTwoFactorAuthenticationIdentityCommand = addTwoFactorAuthenticationIdentityCommand;
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

                    _addTwoFactorAuthenticationIdentityCommand.Execute(identity,
                        _mapper.Map<CreateAdminAuthenticationIdentityAdto, AddTwoFactorAuthenticationIdentityDdto>(
                            createAdminAuthenticationIdentityAdto));
                    
                    Message.Send(AdminIdentityCreatedMessage.Create(createAdminAuthenticationIdentityAdto.ApplicationSystemName, identity.Id));
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


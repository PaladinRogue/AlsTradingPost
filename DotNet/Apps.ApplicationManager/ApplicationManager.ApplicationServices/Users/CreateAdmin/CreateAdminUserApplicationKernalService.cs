using ApplicationManager.ApplicationServices.Users.Models;
using ApplicationManager.Domain.Identities;
using ApplicationManager.Domain.Users;
using ApplicationManager.Domain.Users.Create;
using Common.Application.Exceptions;
using Common.Application.Transactions;
using Common.Domain.Exceptions;
using Common.Domain.Persistence;

namespace ApplicationManager.ApplicationServices.Users.CreateAdmin
{
    public class CreateAdminUserApplicationKernalService : ICreateAdminUserApplicationKernalService
    {
        private readonly ITransactionManager _transactionManager;

        private readonly ICommandRepository<User> _commandRepository;

        private readonly IQueryRepository<Identity> _queryRepository;

        private readonly ICreateUserCommand _createUserCommand;

        public CreateAdminUserApplicationKernalService(
            ITransactionManager transactionManager,
            ICommandRepository<User> commandRepository,
            IQueryRepository<Identity> queryRepository,
            ICreateUserCommand createUserCommand)
        {
            _transactionManager = transactionManager;
            _commandRepository = commandRepository;
            _queryRepository = queryRepository;
            _createUserCommand = createUserCommand;
        }

        public void Create(CreateUserAdto createUserAdto)
        {
            using (ITransaction transaction = _transactionManager.Create())
            {
                Identity identity = _queryRepository.GetById(createUserAdto.IdentityId);

                try
                {
                    User user = _createUserCommand.Execute(new CreateUserDdto
                    {
                        Identity = identity
                    });

                    _commandRepository.Add(user);
                }
                catch (CreateDomainException e)
                {
                    throw new BusinessApplicationException(ExceptionType.Unknown, e);
                }
                catch (ConcurrencyDomainException e)
                {
                    throw new BusinessApplicationException(ExceptionType.Concurrency, e);
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
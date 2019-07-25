using System.Threading.Tasks;
using Authentication.ApplicationServices.Users.Models;
using Authentication.Domain.Identities;
using Authentication.Domain.Users;
using Authentication.Domain.Users.Create;
using Common.ApplicationServices.Exceptions;
using Common.ApplicationServices.Transactions;
using Common.Domain.Exceptions;
using Common.Domain.Persistence;

namespace Authentication.ApplicationServices.Users.CreateAdmin
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

        public async Task CreateAsync(CreateUserAdto createUserAdto)
        {
            using (ITransaction transaction = _transactionManager.Create())
            {
                Identity identity = await _queryRepository.GetByIdAsync(createUserAdto.IdentityId);

                try
                {
                    User user = await _createUserCommand.ExecuteAsync(new CreateUserCommandDdto
                    {
                        Identity = identity
                    });

                    await _commandRepository.AddAsync(user);
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
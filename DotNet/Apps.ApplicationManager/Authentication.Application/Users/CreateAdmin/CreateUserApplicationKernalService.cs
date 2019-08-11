using System.Threading.Tasks;
using PaladinRogue.Authentication.Application.Users.Models;
using PaladinRogue.Authentication.Domain.Identities;
using PaladinRogue.Authentication.Domain.Users;
using PaladinRogue.Authentication.Domain.Users.Create;
using PaladinRogue.Libray.Core.Application.Exceptions;
using PaladinRogue.Libray.Core.Application.Transactions;
using PaladinRogue.Libray.Core.Domain.Exceptions;
using PaladinRogue.Libray.Core.Domain.Persistence;

namespace PaladinRogue.Authentication.Application.Users.CreateAdmin
{
    public class CreateUserApplicationKernalService : ICreateUserApplicationKernalService
    {
        private readonly ITransactionManager _transactionManager;

        private readonly ICommandRepository<User> _commandRepository;

        private readonly IQueryRepository<Identity> _queryRepository;

        private readonly ICreateUserCommand _createUserCommand;

        public CreateUserApplicationKernalService(
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
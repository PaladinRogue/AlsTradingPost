using System.Threading.Tasks;
using PaladinRogue.Gateway.Domain.Applications.Change;
using PaladinRogue.Gateway.Domain.Applications.Create;
using PaladinRogue.Library.Core.Application.Exceptions;
using PaladinRogue.Library.Core.Application.Transactions;
using PaladinRogue.Library.Core.Domain.Exceptions;
using PaladinRogue.Library.Core.Domain.Persistence;

namespace PaladinRogue.Gateway.Application.Applications.Register
{
    public class RegisterApplicationKernalService : IRegisterApplicationKernalService
    {
        private readonly ITransactionManager _transactionManager;

        private readonly ICommandRepository<Domain.Applications.Application> _commandRepository;

        private readonly IChangeApplicationCommand _changeApplicationCommand;

        private readonly ICreateApplicationCommand _createApplicationCommand;

        public RegisterApplicationKernalService(
            ITransactionManager transactionManager,
            ICommandRepository<Domain.Applications.Application> commandRepository,
            IChangeApplicationCommand changeApplicationCommand,
            ICreateApplicationCommand createApplicationCommand)
        {
            _transactionManager = transactionManager;
            _commandRepository = commandRepository;
            _changeApplicationCommand = changeApplicationCommand;
            _createApplicationCommand = createApplicationCommand;
        }

        public async Task RegisterAsync(RegisterApplicationAdto registerApplicationAdto)
        {
            using (ITransaction transaction = _transactionManager.Create())
            {
                try
                {
                    Domain.Applications.Application application = await _commandRepository.GetSingleAsync(a => a.SystemName == registerApplicationAdto.SystemName);
                    if (application == null)
                    {
                        application = await _createApplicationCommand.ExecuteAsync(new CreateApplicationDdto
                        {
                            Name = registerApplicationAdto.Name,
                            SystemName = registerApplicationAdto.SystemName,
                            HostUri = registerApplicationAdto.HostUri
                        });

                        await _commandRepository.AddAsync(application);
                    }
                    else
                    {

                        await _changeApplicationCommand.ExecuteAsync(application, new ChangeApplicationDdto
                        {
                            Name = registerApplicationAdto.Name,
                            HostUri = registerApplicationAdto.HostUri
                        });
                    }

                    transaction.Commit();
                }
                catch (CreateDomainException e)
                {
                    throw new BusinessApplicationException(ExceptionType.Unknown, e);
                }
                catch (ConcurrencyDomainException e)
                {
                    throw new BusinessApplicationException(ExceptionType.Unknown, e);
                }
            }
        }
    }
}
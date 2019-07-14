using System.Threading.Tasks;
using Common.ApplicationServices.Exceptions;
using Common.ApplicationServices.Transactions;
using Common.Domain.Exceptions;
using Common.Domain.Persistence;
using ReverseProxy.Domain.Applications;
using ReverseProxy.Domain.Applications.Change;
using ReverseProxy.Domain.Applications.Create;

namespace ReverseProxy.ApplicationServices.Applications.Register
{
    public class RegisterApplicationKernalService : IRegisterApplicationKernalService
    {
        private readonly ITransactionManager _transactionManager;

        private readonly ICommandRepository<Application> _commandRepository;

        private readonly IChangeApplicationCommand _changeApplicationCommand;

        private readonly ICreateApplicationCommand _createApplicationCommand;

        public RegisterApplicationKernalService(
            ITransactionManager transactionManager,
            ICommandRepository<Application> commandRepository,
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
                    Application application = await _commandRepository.GetSingleAsync(a => a.SystemName == registerApplicationAdto.SystemName);
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
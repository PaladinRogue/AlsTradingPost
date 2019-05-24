using ApplicationManager.ApplicationServices.Applications.Interfaces;
using ApplicationManager.ApplicationServices.Applications.Models;
using ApplicationManager.Domain.Applications;
using Common.Application.Exceptions;
using Common.Application.Transactions;
using Common.ApplicationServices.Services.Command;
using Common.Domain.Exceptions;
using Common.Domain.Persistence;

namespace ApplicationManager.ApplicationServices.Applications
{
    public class RegisterApplicationKernalService : IRegisterApplicationKernalService
    {
        private readonly ITransactionManager _transactionManager;

        private readonly IQueryRepository<Application> _queryRepository;

        private readonly ICommandService<Application> _commandService;

        public RegisterApplicationKernalService(
            ITransactionManager transactionManager,
            IQueryRepository<Application> queryRepository,
            ICommandService<Application> commandService)
        {
            _transactionManager = transactionManager;
            _queryRepository = queryRepository;
            _commandService = commandService;
        }

        public void Register(RegisterApplicationAdto registerApplicationAdto)
        {
            using (ITransaction transaction = _transactionManager.Create())
            {
                Application application = _queryRepository.GetSingle(a => a.Name == registerApplicationAdto.Name);

                if (application == null)
                {
                    try
                    {
                        _commandService.Create(Application.Create(new CreateApplicationDdto
                        {
                            Name = registerApplicationAdto.Name,
                            SystemName = registerApplicationAdto.SystemName
                        }));
                    }
                    catch (CreateDomainException e)
                    {
                        throw new BusinessApplicationException(ExceptionType.None, e);
                    }
                    catch (ConcurrencyDomainException e)
                    {
                        throw new BusinessApplicationException(ExceptionType.None, e);
                    }
                }
                else
                {
                    application.Change(new ChangeApplictionDdto
                    {
                        Name = registerApplicationAdto.Name,
                        SystemName = registerApplicationAdto.SystemName
                    });
                }

                transaction.Commit();
            }
        }
    }
}


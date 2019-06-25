using ApplicationManager.ApplicationServices.Identities.CreateAdmin;
using ApplicationManager.Domain.Applications;
using ApplicationManager.Domain.Applications.Change;
using ApplicationManager.Domain.Applications.Create;
using Common.ApplicationServices.Exceptions;
using Common.ApplicationServices.Transactions;
using Common.Domain.Exceptions;
using Common.Domain.Persistence;

namespace ApplicationManager.ApplicationServices.Applications.Register
{
    public class RegisterApplicationKernalService : IRegisterApplicationKernalService
    {
        private readonly ITransactionManager _transactionManager;

        private readonly IQueryRepository<Application> _queryRepository;

        private readonly ICreateAdminAuthenticationIdentityKernalService _createAdminAuthenticationIdentityKernalService;

        private readonly ICommandRepository<Application> _commandRepository;

        private readonly IChangeApplicationCommand _changeApplicationCommand;

        private readonly ICreateApplicationCommand _createApplicationCommand;

        public RegisterApplicationKernalService(
            ITransactionManager transactionManager,
            IQueryRepository<Application> queryRepository,
            ICommandRepository<Application> commandRepository,
            ICreateAdminAuthenticationIdentityKernalService createAdminAuthenticationIdentityKernalService,
            IChangeApplicationCommand changeApplicationCommand,
            ICreateApplicationCommand createApplicationCommand)
        {
            _transactionManager = transactionManager;
            _queryRepository = queryRepository;
            _commandRepository = commandRepository;
            _createAdminAuthenticationIdentityKernalService = createAdminAuthenticationIdentityKernalService;
            _changeApplicationCommand = changeApplicationCommand;
            _createApplicationCommand = createApplicationCommand;
        }

        public void Register(RegisterApplicationAdto registerApplicationAdto)
        {
            using (ITransaction transaction = _transactionManager.Create())
            {
                Application application = _queryRepository.GetSingle(a => a.SystemName == registerApplicationAdto.SystemName);

                if (application == null)
                {
                    try
                    {
                        application = _createApplicationCommand.Execute(new CreateApplicationDdto
                        {
                            Name = registerApplicationAdto.Name,
                            SystemName = registerApplicationAdto.SystemName
                        });

                        _commandRepository.Add(application);

                        if (!string.IsNullOrWhiteSpace(registerApplicationAdto.AdminEmailAddress))
                        {
                            _createAdminAuthenticationIdentityKernalService.Create(
                                new CreateAdminAuthenticationIdentityAdto
                                {
                                    EmailAddress = registerApplicationAdto.AdminEmailAddress,
                                    ApplicationSystemName = registerApplicationAdto.SystemName
                                });
                        }
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
                else
                {
                    _changeApplicationCommand.Execute(application, new ChangeApplicationDdto
                    {
                        Name = registerApplicationAdto.Name
                    });
                }

                transaction.Commit();
            }
        }
    }
}


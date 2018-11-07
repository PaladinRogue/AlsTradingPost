using ApplicationManager.ApplicationServices.Applications.Models;
using ApplicationManager.Domain.Applications;
using Common.Application.Exceptions;
using Common.Application.Transactions;
using Common.Domain.Exceptions;
using Common.Domain.Persistence;

namespace ApplicationManager.ApplicationServices.Identities
{
    public class CreateIdentityKernalService : ICreateIdentityKernalService
    {
        private readonly ITransactionManager _transactionManager;

        private readonly IRepository<Application> _repository;

        public CreateIdentityKernalService(
            ITransactionManager transactionManager,
            IRepository<Application> repository)
        {
            _transactionManager = transactionManager;
            _repository = repository;
        }

        public void Register(RegisterApplicationAdto registerApplicationAdto)
        {
            using (ITransaction transaction = _transactionManager.Create())
            {
                Application application = _repository.GetSingle(a => a.Name == registerApplicationAdto.Name);

                if (application == null)
                {
                    try
                    {
                        _repository.Add(Application.Create(new CreateApplicationDdto
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

    public interface ICreateIdentityKernalService
    {
    }
}


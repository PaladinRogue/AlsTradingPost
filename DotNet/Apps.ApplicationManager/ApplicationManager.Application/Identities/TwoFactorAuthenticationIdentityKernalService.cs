using ApplicationManager.ApplicationServices.Identities.Interfaces;
using ApplicationManager.ApplicationServices.Identities.Models;
using ApplicationManager.Domain.Identities;
using Common.Application.Transactions;
using Common.ApplicationServices.Services.Command;

namespace ApplicationManager.ApplicationServices.Identities
{
    public class TwoFactorAuthenticationIdentityKernalService : ITwoFactorAuthenticationIdentityKernalService
    {
        private readonly ICommandService<Identity> _identityCommandService;

        private readonly ITransactionManager _transactionManager;

        public TwoFactorAuthenticationIdentityKernalService(
            ICommandService<Identity> identityCommandService,
            ITransactionManager transactionManager)
        {
            _identityCommandService = identityCommandService;
            _transactionManager = transactionManager;
        }

        public void Create(CreateTwoFactorAuthenticationIdentityAdto createTwoFactorAuthenticationIdentityAdto)
        {
            using (ITransaction transaction = _transactionManager.Create())
            {
                Identity identity = Identity.Create();
                _identityCommandService.Create(identity);

                transaction.Commit();
            }
        }
    }
}


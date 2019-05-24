using System;
using ApplicationManager.ApplicationServices.Identities.Interfaces;
using ApplicationManager.ApplicationServices.Identities.Models;
using ApplicationManager.Domain.AuthenticationServices.Identities;
using ApplicationManager.Domain.Identities;
using Common.Application.Transactions;
using Common.ApplicationServices.Services.Command;

namespace ApplicationManager.ApplicationServices.Identities
{
    public class PasswordIdentityKernalService : IPasswordIdentityKernalService
    {
        private readonly ICommandService<Identity> _identityCommandService;

        private readonly ICommandService<PasswordIdentity> _passwordIdentityCommandService;

        private readonly ITransactionManager _transactionManager;

        public PasswordIdentityKernalService(
            ICommandService<Identity> identityCommandService,
            ITransactionManager transactionManager,
            ICommandService<PasswordIdentity> passwordIdentityCommandService)
        {
            _identityCommandService = identityCommandService;
            _transactionManager = transactionManager;
            _passwordIdentityCommandService = passwordIdentityCommandService;
        }

        public void Create(CreatePasswordIdentityAdto createPasswordIdentityAdto)
        {
            using (ITransaction transaction = _transactionManager.Create())
            {
                Identity identity = Identity.Create();
                _identityCommandService.Create(identity);
            }
        }
    }
}


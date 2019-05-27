using ApplicationManager.ApplicationServices.Identities.Interfaces;
using ApplicationManager.ApplicationServices.Identities.Settings;
using ApplicationManager.Domain.Identities;
using ApplicationManager.Domain.Identities.AuthenticationIdentities;
using Common.Application.Exceptions;
using Common.Application.Transactions;
using Common.ApplicationServices.Services.Command;
using Common.Domain.Exceptions;
using Microsoft.Extensions.Options;

namespace ApplicationManager.ApplicationServices.Identities
{
    public class CreateAdminAuthenticationIdentityKernalService : ICreateAdminAuthenticationIdentityKernalService
    {
        private readonly ICommandService<Identity> _identityCommandService;

        private readonly ITransactionManager _transactionManager;
        private readonly SystemAdminIdentitySettings _systemAdminIdentitySettings;
        private readonly ICreateTwoFactorAuthenticationIdentityCommand _createTwoFactorAuthenticationIdentityCommand;

        public CreateAdminAuthenticationIdentityKernalService(
            ICommandService<Identity> identityCommandService,
            ITransactionManager transactionManager,
            IOptions<SystemAdminIdentitySettings> systemAdminIdentitySettingsAccessor,
            ICreateTwoFactorAuthenticationIdentityCommand createTwoFactorAuthenticationIdentityCommand)
        {
            _identityCommandService = identityCommandService;
            _transactionManager = transactionManager;
            _createTwoFactorAuthenticationIdentityCommand = createTwoFactorAuthenticationIdentityCommand;
            _systemAdminIdentitySettings = systemAdminIdentitySettingsAccessor.Value;
        }

        public void Create()
        {
            using (ITransaction transaction = _transactionManager.Create())
            {
                Identity identity = Identity.Create();
                _identityCommandService.Create(identity);

                try
                {
                    _createTwoFactorAuthenticationIdentityCommand.Execute(identity,
                        new CreateTwoFactorAuthenticationIdentityDdto
                        {
                            EmailAddress = _systemAdminIdentitySettings.Email
                        });
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


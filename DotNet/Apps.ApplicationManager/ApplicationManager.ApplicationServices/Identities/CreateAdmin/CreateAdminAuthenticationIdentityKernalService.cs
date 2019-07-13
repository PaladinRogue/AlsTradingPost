using System.Threading.Tasks;
using ApplicationManager.Domain.AuthenticationServices;
using ApplicationManager.Domain.Identities;
using ApplicationManager.Domain.Identities.Create;
using ApplicationManager.Domain.Identities.ForgotPassword;
using ApplicationManager.Domain.Identities.RegisterPassword;
using Common.ApplicationServices;
using Common.ApplicationServices.Exceptions;
using Common.ApplicationServices.Transactions;
using Common.Domain.Exceptions;
using Common.Domain.Persistence;
using Common.Messaging.Infrastructure;
using Common.Messaging.Messages;
using String = Common.Resources.Extensions.String;

namespace ApplicationManager.ApplicationServices.Identities.CreateAdmin
{
    public class CreateAdminAuthenticationIdentityKernalService : ICreateAdminAuthenticationIdentityKernalService
    {
        private readonly ITransactionManager _transactionManager;

        private readonly ICreateIdentityCommand _createIdentityCommand;

        private readonly IRegisterPasswordCommand _registerPasswordCommand;

        private readonly IForgotPasswordCommand _forgotPasswordCommand;

        private readonly ICommandRepository<Identity> _commandRepository;

        private readonly ICommandRepository<AuthenticationService> _authenticationServiceCommandRepository;

        public CreateAdminAuthenticationIdentityKernalService(
            ITransactionManager transactionManager,
            ICreateIdentityCommand createIdentityCommand,
            ICommandRepository<Identity> commandRepository,
            IRegisterPasswordCommand registerPasswordCommand,
            ICommandRepository<AuthenticationService> authenticationServiceCommandRepository,
            IForgotPasswordCommand forgotPasswordCommand)
        {
            _transactionManager = transactionManager;
            _createIdentityCommand = createIdentityCommand;
            _commandRepository = commandRepository;
            _registerPasswordCommand = registerPasswordCommand;
            _authenticationServiceCommandRepository = authenticationServiceCommandRepository;
            _forgotPasswordCommand = forgotPasswordCommand;
        }

        public async Task CreateAsync(CreateAdminAuthenticationIdentityAdto createAdminAuthenticationIdentityAdto)
        {
            using (ITransaction transaction = _transactionManager.Create())
            {
                try
                {
                    Identity identity = await _createIdentityCommand.ExecuteAsync();

                    string tempPassword = $"{String.RandomChar(20)}{String.RandomNumeric(3)}{String.RandomSpecial(3)}";

                    await _registerPasswordCommand.ExecuteAsync(identity, await GetAuthenticationGrantTypePassword(), new RegisterPasswordCommandDdto
                    {
                        Identifier = createAdminAuthenticationIdentityAdto.EmailAddress,
                        EmailAddress = createAdminAuthenticationIdentityAdto.EmailAddress,
                        Password = tempPassword,
                        ConfirmPassword = tempPassword
                    });

                    await _commandRepository.AddAsync(identity);

                    await _forgotPasswordCommand.ExecuteAsync(new ForgotPasswordCommandDdto
                    {
                        EmailAddress = createAdminAuthenticationIdentityAdto.EmailAddress
                    });

                    await _commandRepository.UpdateAsync(identity);

                    await Message.SendAsync(CreateAdminIdentityMessage.Create(createAdminAuthenticationIdentityAdto.ApplicationSystemName, identity.Id));

                    transaction.Commit();
                }
                catch (DomainValidationRuleException e)
                {
                    throw new BusinessValidationRuleApplicationException(e.ValidationResult);
                }
            }
        }

        private async Task<AuthenticationGrantTypePassword> GetAuthenticationGrantTypePassword()
        {
            AuthenticationService authenticationService = await _authenticationServiceCommandRepository.GetSingleAsync(s => s is AuthenticationGrantTypePassword);

            if (!(authenticationService is AuthenticationGrantTypePassword authenticationGrantTypePassword))
            {
                throw new BusinessApplicationException(ExceptionType.BadRequest, ErrorCodes.PasswordLoginNotConfigured, "Password identities are not configured");
            }

            return authenticationGrantTypePassword;
        }
    }
}


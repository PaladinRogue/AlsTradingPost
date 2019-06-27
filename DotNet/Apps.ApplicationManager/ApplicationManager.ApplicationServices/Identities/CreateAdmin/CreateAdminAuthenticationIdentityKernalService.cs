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

        public void Create(CreateAdminAuthenticationIdentityAdto createAdminAuthenticationIdentityAdto)
        {
            using (ITransaction transaction = _transactionManager.Create())
            {
                try
                {
                    Identity identity = _createIdentityCommand.Execute(new CreateIdentityCommandDdto
                    {
                        EmailAddress = createAdminAuthenticationIdentityAdto.EmailAddress
                    });

                    string tempPassword = $"{String.RandomChar(20)}{String.RandomNumeric(3)}{String.RandomSpecial(3)}";

                    _registerPasswordCommand.Execute(identity, GetAuthenticationGrantTypePassword(), new RegisterPasswordCommandDdto
                    {
                        Identifier = createAdminAuthenticationIdentityAdto.EmailAddress,
                        EmailAddress = createAdminAuthenticationIdentityAdto.EmailAddress,
                        Password = tempPassword,
                        ConfirmPassword = tempPassword
                    });

                    _commandRepository.Add(identity);

                    _forgotPasswordCommand.Execute(new ForgotPasswordCommandDdto
                    {
                        EmailAddress = createAdminAuthenticationIdentityAdto.EmailAddress
                    });

                    _commandRepository.Update(identity);

                    Message.Send(AdminIdentityCreatedMessage.Create(createAdminAuthenticationIdentityAdto.ApplicationSystemName, identity.Id));
                }
                catch (DomainValidationRuleException e)
                {
                    throw new BusinessValidationRuleApplicationException(e.ValidationResult);
                }

                transaction.Commit();
            }
        }

        private AuthenticationGrantTypePassword GetAuthenticationGrantTypePassword()
        {
            AuthenticationService authenticationService = _authenticationServiceCommandRepository.GetSingle(s => s is AuthenticationGrantTypePassword);

            if (!(authenticationService is AuthenticationGrantTypePassword authenticationGrantTypePassword))
            {
                throw new BusinessApplicationException(ExceptionType.BadRequest, ErrorCodes.PasswordLoginNotConfigured, "Password identities are not configured");
            }

            return authenticationGrantTypePassword;
        }
    }
}


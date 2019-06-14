using System;
using ApplicationManager.ApplicationServices.Identities.Models;
using ApplicationManager.Domain.AuthenticationServices;
using ApplicationManager.Domain.Identities;
using ApplicationManager.Domain.Identities.Commands;
using ApplicationManager.Domain.Identities.Models;
using Common.Application.Exceptions;
using Common.Application.Transactions;
using Common.Domain.Persistence;

namespace ApplicationManager.ApplicationServices.Identities
{
    public class IdentityApplicationService : IIdentityApplicationService
    {
        private readonly ITransactionManager _transactionManager;

        private readonly IAddConfirmedPasswordIdentityCommand _addConfirmedPasswordIdentityCommand;

        private readonly ICommandRepository<AuthenticationService> _authenticationServiceCommandRepository;

        private readonly ICommandRepository<Identity> _identityCommandRepository;

        public IdentityApplicationService(
            ITransactionManager transactionManager,
            IAddConfirmedPasswordIdentityCommand addConfirmedPasswordIdentityCommand,
            ICommandRepository<Identity> identityCommandRepository,
            ICommandRepository<AuthenticationService> authenticationServiceCommandRepository)
        {
            _transactionManager = transactionManager;
            _addConfirmedPasswordIdentityCommand = addConfirmedPasswordIdentityCommand;
            _identityCommandRepository = identityCommandRepository;
            _authenticationServiceCommandRepository = authenticationServiceCommandRepository;
        }

        public PasswordIdentityAdto CreateConfirmedPasswordIdentity(CreateConfirmedPasswordIdentityAdto createConfirmedPasswordIdentityAdto)
        {
            if (createConfirmedPasswordIdentityAdto == null)
            {
                throw new ArgumentNullException(nameof(createConfirmedPasswordIdentityAdto));
            }

            using (ITransaction transaction = _transactionManager.Create())
            {
                AuthenticationService authenticationService = _authenticationServiceCommandRepository.GetSingle(s => s is AuthenticationGrantTypePassword);

                if (!(authenticationService is AuthenticationGrantTypePassword authenticationGrantTypePassword))
                {
                    throw new BusinessApplicationException(ExceptionType.BadRequest, "Password identities are not configured");
                }

                Identity identity = _identityCommandRepository.GetById(createConfirmedPasswordIdentityAdto.IdentityId);

                if (identity == null)
                {
                    throw new BusinessApplicationException(ExceptionType.NotFound, "No identity exists for given id");
                }

                PasswordIdentity passwordIdentity = _addConfirmedPasswordIdentityCommand.Execute(identity, authenticationGrantTypePassword, new AddConfirmedPasswordIdentityDdto
                {
                    Token = createConfirmedPasswordIdentityAdto.Token,
                    Identifier = createConfirmedPasswordIdentityAdto.Identifier,
                    Password = createConfirmedPasswordIdentityAdto.Password,
                    ConfirmPassword = createConfirmedPasswordIdentityAdto.ConfirmPassword
                });

                _identityCommandRepository.Update(identity);

                transaction.Commit();

                return new PasswordIdentityAdto
                {
                    Id = passwordIdentity.Id,
                    Identifier = passwordIdentity.Identifier,
                    Password = passwordIdentity.PasswordMask
                };
            }
        }
    }
}
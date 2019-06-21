using System;
using System.Linq;
using ApplicationManager.ApplicationServices.Identities.Models;
using ApplicationManager.Domain.AuthenticationServices;
using ApplicationManager.Domain.Identities;
using ApplicationManager.Domain.Identities.AddConfirmedPassword;
using ApplicationManager.Domain.Identities.ChangePassword;
using ApplicationManager.Domain.Identities.ValidateTwoFactor;
using Common.Application.Exceptions;
using Common.Application.Transactions;
using Common.ApplicationServices.Concurrency;
using Common.Domain.Exceptions;
using Common.Domain.Persistence;

namespace ApplicationManager.ApplicationServices.Identities
{
    public class IdentityApplicationService : IIdentityApplicationService
    {
        private readonly ITransactionManager _transactionManager;

        private readonly IAddConfirmedPasswordIdentityCommand _addConfirmedPasswordIdentityCommand;

        private readonly ICommandRepository<AuthenticationService> _authenticationServiceCommandRepository;

        private readonly ICommandRepository<Identity> _identityCommandRepository;

        private readonly IQueryRepository<Identity> _identityQueryRepository;

        private readonly IChangePasswordCommand _changePasswordCommand;

        public IdentityApplicationService(
            ITransactionManager transactionManager,
            IAddConfirmedPasswordIdentityCommand addConfirmedPasswordIdentityCommand,
            ICommandRepository<Identity> identityCommandRepository,
            ICommandRepository<AuthenticationService> authenticationServiceCommandRepository,
            IQueryRepository<Identity> identityQueryRepository,
            IChangePasswordCommand changePasswordCommand)
        {
            _transactionManager = transactionManager;
            _addConfirmedPasswordIdentityCommand = addConfirmedPasswordIdentityCommand;
            _identityCommandRepository = identityCommandRepository;
            _authenticationServiceCommandRepository = authenticationServiceCommandRepository;
            _identityQueryRepository = identityQueryRepository;
            _changePasswordCommand = changePasswordCommand;
        }

        public IdentityAdto Get(GetIdentityAdto getIdentityAdto)
        {
            if (getIdentityAdto == null) throw new ArgumentNullException(nameof(getIdentityAdto));

            using (ITransaction transaction = _transactionManager.Create())
            {
                Identity identity = _identityQueryRepository.GetById(getIdentityAdto.Id);

                if (identity == null)
                {
                    throw new BusinessApplicationException(ExceptionType.NotFound, "Identity does not exist");
                }

                transaction.Commit();

                return new IdentityAdto
                {
                    Id = identity.Id,
                    Version = ConcurrencyVersionFactory.CreateFromEntity(identity)
                };
            }
        }

        public PasswordIdentityAdto CreateConfirmedPasswordIdentity(CreateConfirmedPasswordIdentityAdto createConfirmedPasswordIdentityAdto)
        {
            if (createConfirmedPasswordIdentityAdto == null) throw new ArgumentNullException(nameof(createConfirmedPasswordIdentityAdto));

            using (ITransaction transaction = _transactionManager.Create())
            {
                try
                {
                    Identity identity = _identityCommandRepository.GetWithConcurrencyCheck(createConfirmedPasswordIdentityAdto.IdentityId, createConfirmedPasswordIdentityAdto.Version);

                    AuthenticationService authenticationService = _authenticationServiceCommandRepository.GetSingle(s => s is AuthenticationGrantTypePassword);

                    if (!(authenticationService is AuthenticationGrantTypePassword authenticationGrantTypePassword))
                    {
                        throw new BusinessApplicationException(ExceptionType.BadRequest, "Password identities are not configured");
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
                        Identifier = passwordIdentity.Identifier,
                        Password = passwordIdentity.Password,
                        Version = ConcurrencyVersionFactory.CreateFromEntity(identity)
                    };
                }
                catch (PasswordIdentityExistsDomainException)
                {
                    throw new BusinessApplicationException(ExceptionType.Conflict, "Password already set");
                }
                catch (InvalidTwoFactorTokenDomainException)
                {
                    throw new BusinessApplicationException(ExceptionType.Unauthorized, "Two factor token is not recognized");
                }
                catch (ConcurrencyDomainException)
                {
                    throw new BusinessApplicationException(ExceptionType.Concurrency, "Concurrency check failed");
                }
                catch (DomainValidationRuleException e)
                {
                    throw new BusinessValidationRuleApplicationException(e.ValidationResult);
                }
            }
        }

        public PasswordIdentityAdto GetPasswordIdentity(GetPasswordIdentityAdto getPasswordIdentityAdto)
        {
            if (getPasswordIdentityAdto == null) throw new ArgumentNullException(nameof(getPasswordIdentityAdto));

            using (ITransaction transaction = _transactionManager.Create())
            {
                Identity identity = _identityQueryRepository.GetById(getPasswordIdentityAdto.IdentityId);

                if (identity == null)
                {
                    throw new BusinessApplicationException(ExceptionType.NotFound, "Identity does not exist");
                }

                PasswordIdentity passwordIdentity = identity.AuthenticationIdentities.OfType<PasswordIdentity>().SingleOrDefault();

                if (passwordIdentity == null)
                {
                    throw new BusinessApplicationException(ExceptionType.NotFound, "Password not set for identity");
                }

                transaction.Commit();

                return new PasswordIdentityAdto
                {
                    Identifier = passwordIdentity.Identifier,
                    Password = passwordIdentity.Password,
                    Version = ConcurrencyVersionFactory.CreateFromEntity(identity)
                };
            }
        }

        public PasswordIdentityAdto ChangePassword(ChangePasswordAdto changePasswordAdto)
        {
            if (changePasswordAdto == null) throw new ArgumentNullException(nameof(changePasswordAdto));

            using (ITransaction transaction = _transactionManager.Create())
            {
                try
                {
                    Identity identity = _identityCommandRepository.GetWithConcurrencyCheck(changePasswordAdto.IdentityId, changePasswordAdto.Version);

                    if (identity == null)
                    {
                        throw new BusinessApplicationException(ExceptionType.NotFound, "Identity does not exist");
                    }

                    _changePasswordCommand.Execute(identity, new ChangePasswordDdto
                    {
                        Password = changePasswordAdto.Password,
                        ConfirmPassword = changePasswordAdto.ConfirmPassword
                    });

                    _identityCommandRepository.Update(identity);

                    transaction.Commit();

                    return GetPasswordIdentity(new GetPasswordIdentityAdto
                    {
                        IdentityId = identity.Id
                    });
                }
                catch (PasswordNotSetDomainException e)
                {
                    throw new BusinessApplicationException(ExceptionType.NotFound, e);
                }
                catch (ConcurrencyDomainException e)
                {
                    throw new BusinessApplicationException(ExceptionType.Concurrency, e);
                }
                catch (DomainValidationRuleException e)
                {
                    throw new BusinessValidationRuleApplicationException(e.ValidationResult);
                }
            }
        }
    }
}
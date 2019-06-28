using System;
using System.Linq;
using ApplicationManager.ApplicationServices.Identities.Models;
using ApplicationManager.Domain.AuthenticationServices;
using ApplicationManager.Domain.Identities;
using ApplicationManager.Domain.Identities.ChangePassword;
using ApplicationManager.Domain.Identities.ConfirmIdentity;
using ApplicationManager.Domain.Identities.Create;
using ApplicationManager.Domain.Identities.CreateRefreshToken;
using ApplicationManager.Domain.Identities.ForgotPassword;
using ApplicationManager.Domain.Identities.Logout;
using ApplicationManager.Domain.Identities.RegisterPassword;
using ApplicationManager.Domain.Identities.ResetPassword;
using ApplicationManager.Domain.Identities.ValidateToken;
using Common.ApplicationServices;
using Common.ApplicationServices.Concurrency;
using Common.ApplicationServices.Exceptions;
using Common.ApplicationServices.Transactions;
using Common.Domain.Exceptions;
using Common.Domain.Persistence;

namespace ApplicationManager.ApplicationServices.Identities
{
    public class IdentityApplicationService : IIdentityApplicationService
    {
        private readonly ITransactionManager _transactionManager;

        private readonly IResetPasswordCommand _resetPasswordCommand;

        private readonly ICommandRepository<AuthenticationService> _authenticationServiceCommandRepository;

        private readonly ICommandRepository<Identity> _identityCommandRepository;

        private readonly IQueryRepository<Identity> _identityQueryRepository;

        private readonly IChangePasswordCommand _changePasswordCommand;

        private readonly IRegisterPasswordCommand _registerPasswordCommand;

        private readonly ICreateIdentityCommand _createIdentityCommand;

        private readonly IForgotPasswordCommand _forgotPasswordCommand;

        private readonly IConfirmIdentityCommand _confirmIdentityCommand;

        private readonly ICreateRefreshTokenCommand _createRefreshTokenCommand;

        private readonly ILogoutCommand _logoutCommand;

        public IdentityApplicationService(
            ITransactionManager transactionManager,
            IResetPasswordCommand resetPasswordCommand,
            ICommandRepository<Identity> identityCommandRepository,
            ICommandRepository<AuthenticationService> authenticationServiceCommandRepository,
            IQueryRepository<Identity> identityQueryRepository,
            IChangePasswordCommand changePasswordCommand,
            IRegisterPasswordCommand registerPasswordCommand,
            ICreateIdentityCommand createIdentityCommand,
            IForgotPasswordCommand forgotPasswordCommand,
            IConfirmIdentityCommand confirmIdentityCommand,
            ICreateRefreshTokenCommand createRefreshTokenCommand,
            ILogoutCommand logoutCommand)
        {
            _transactionManager = transactionManager;
            _resetPasswordCommand = resetPasswordCommand;
            _identityCommandRepository = identityCommandRepository;
            _authenticationServiceCommandRepository = authenticationServiceCommandRepository;
            _identityQueryRepository = identityQueryRepository;
            _changePasswordCommand = changePasswordCommand;
            _registerPasswordCommand = registerPasswordCommand;
            _createIdentityCommand = createIdentityCommand;
            _forgotPasswordCommand = forgotPasswordCommand;
            _confirmIdentityCommand = confirmIdentityCommand;
            _createRefreshTokenCommand = createRefreshTokenCommand;
            _logoutCommand = logoutCommand;
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

        public void ResetPassword(ResetPasswordAdto resetPasswordAdto)
        {
            if (resetPasswordAdto == null) throw new ArgumentNullException(nameof(resetPasswordAdto));

            using (ITransaction transaction = _transactionManager.Create())
            {
                try
                {
                    Identity identity = _resetPasswordCommand.Execute(new ResetPasswordCommandDdto
                    {
                        Token = resetPasswordAdto.Token,
                        Password = resetPasswordAdto.Password,
                        ConfirmPassword = resetPasswordAdto.ConfirmPassword
                    });

                    _identityCommandRepository.Update(identity);

                    transaction.Commit();
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

        public void ForgotPassword(ForgotPasswordAdto forgotPasswordAdto)
        {
            if (forgotPasswordAdto == null) throw new ArgumentNullException(nameof(forgotPasswordAdto));

            using (ITransaction transaction = _transactionManager.Create())
            {
                try
                {
                    Identity identity = _forgotPasswordCommand.Execute(new ForgotPasswordCommandDdto
                    {
                        EmailAddress = forgotPasswordAdto.EmailAddress
                    });

                    if (identity == null)
                    {
                        return;
                    }

                    _identityCommandRepository.Update(identity);

                    transaction.Commit();
                }
                catch (PasswordNotSetDomainException)
                {
                }
                catch (DomainValidationRuleException e)
                {
                    throw new BusinessValidationRuleApplicationException(e.ValidationResult);
                }
            }
        }

        public void ConfirmIdentity(ConfirmIdentityAdto confirmIdentityAdto)
        {
            if (confirmIdentityAdto == null) throw new ArgumentNullException(nameof(confirmIdentityAdto));

            using (ITransaction transaction = _transactionManager.Create())
            {
                try
                {
                    Identity identity = _identityCommandRepository.GetWithConcurrencyCheck(confirmIdentityAdto.IdentityId, confirmIdentityAdto.Version);

                    if (identity == null)
                    {
                        throw new BusinessApplicationException(ExceptionType.Concurrency, "No identity found to confirm identity");
                    }

                    _confirmIdentityCommand.Execute(identity, new ConfirmIdentityCommandDdto
                    {
                        Token = confirmIdentityAdto.Token
                    });

                    _identityCommandRepository.Update(identity);

                    transaction.Commit();
                }
                catch (ConcurrencyDomainException)
                {
                    throw new BusinessApplicationException(ExceptionType.Concurrency, "No identity found to confirm identity");
                }
                catch (InvalidTwoFactorTokenDomainException)
                {
                    throw new BusinessApplicationException(ExceptionType.Unauthorized, "Token invalid for identity");
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

                    _changePasswordCommand.Execute(identity, new ChangePasswordCommandDdto
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

        public PasswordIdentityAdto RegisterPassword(RegisterPasswordAdto registerPasswordAdto)
        {
            if (registerPasswordAdto == null) throw new ArgumentNullException(nameof(registerPasswordAdto));

            using (ITransaction transaction = _transactionManager.Create())
            {
                try
                {
                    Identity identity = _createIdentityCommand.Execute(new CreateIdentityCommandDdto
                    {
                        EmailAddress = registerPasswordAdto.EmailAddress
                    });

                    PasswordIdentity passwordIdentity = _registerPasswordCommand.Execute(identity, GetAuthenticationGrantTypePassword(), new RegisterPasswordCommandDdto
                    {
                        Identifier = registerPasswordAdto.Identifier,
                        Password = registerPasswordAdto.Password,
                        ConfirmPassword = registerPasswordAdto.ConfirmPassword,
                        EmailAddress = registerPasswordAdto.EmailAddress
                    });

                    _identityCommandRepository.Add(identity);

                    transaction.Commit();

                    return new PasswordIdentityAdto
                    {
                        IdentityId = identity.Id,
                        Identifier = passwordIdentity.Identifier,
                        Password = passwordIdentity.Password,
                        Version = ConcurrencyVersionFactory.CreateFromEntity(identity)
                    };
                }
                catch (DomainValidationRuleException e)
                {
                    throw new BusinessValidationRuleApplicationException(e.ValidationResult);
                }
            }
        }

        public RefreshTokenIdentityAdto CreateRefreshToken(CreateRefreshTokenAdto createRefreshTokenAdto)
        {
            using (ITransaction transaction = _transactionManager.Create())
            {
                try
                {
                    Identity identity = _identityCommandRepository.GetById(createRefreshTokenAdto.IdentityId);

                    if (identity == null)
                    {
                        throw new BusinessApplicationException(ExceptionType.NotFound, "Identity not found");
                    }

                    AuthenticationGrantTypeRefreshToken authenticationGrantTypeRefreshToken =
                        (AuthenticationGrantTypeRefreshToken) _authenticationServiceCommandRepository.GetSingle(a => a is AuthenticationGrantTypeRefreshToken);

                    if (authenticationGrantTypeRefreshToken == null)
                    {
                        throw new BusinessApplicationException(ExceptionType.NotFound, "Refresh tokens are not configured");
                    }

                    RefreshTokenIdentityDdto refreshTokenIdentityDdto = _createRefreshTokenCommand.Execute(identity, authenticationGrantTypeRefreshToken);

                    _identityCommandRepository.Update(identity);

                    transaction.Commit();

                    return new RefreshTokenIdentityAdto
                    {
                        Token = refreshTokenIdentityDdto.Token
                    };
                }
                catch (DomainValidationRuleException e)
                {
                    throw new BusinessValidationRuleApplicationException(e.ValidationResult);
                }
            }
        }

        public void Logout(LogoutAdto logoutAdto)
        {
            using (ITransaction transaction = _transactionManager.Create())
            {
                Identity identity = _identityCommandRepository.GetById(logoutAdto.IdentityId);

                if (identity == null)
                {
                    throw new BusinessApplicationException(ExceptionType.NotFound, "Identity does not exist");
                }

                _logoutCommand.Execute(identity);

                _identityCommandRepository.Update(identity);

                transaction.Commit();
            }
        }

        private AuthenticationGrantTypePassword GetAuthenticationGrantTypePassword()
        {
            AuthenticationService authenticationService = _authenticationServiceCommandRepository.GetSingle(s => s is AuthenticationGrantTypePassword);

            if (!(authenticationService is AuthenticationGrantTypePassword authenticationGrantTypePassword))
            {
                throw new BusinessApplicationException(ExceptionType.BadRequest, ErrorCodes.PasswordLoginNotConfigured, "Password logins are not configured");
            }

            return authenticationGrantTypePassword;
        }
    }
}
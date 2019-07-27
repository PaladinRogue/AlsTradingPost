using System.Threading.Tasks;
using Authentication.ApplicationServices.Identities.Models;
using Authentication.Domain.AuthenticationServices;
using Authentication.Domain.Identities;
using Authentication.Domain.Identities.ChangePassword;
using Authentication.Domain.Identities.ConfirmIdentity;
using Authentication.Domain.Identities.Create;
using Authentication.Domain.Identities.CreateRefreshToken;
using Authentication.Domain.Identities.ForgotPassword;
using Authentication.Domain.Identities.Logout;
using Authentication.Domain.Identities.RegisterPassword;
using Authentication.Domain.Identities.ResendConfirmIdentity;
using Authentication.Domain.Identities.ResetPassword;
using Authentication.Domain.Identities.ValidateToken;
using Common.ApplicationServices;
using Common.ApplicationServices.Concurrency;
using Common.ApplicationServices.Exceptions;
using Common.ApplicationServices.Transactions;
using Common.Domain.Exceptions;
using Common.Domain.Persistence;

namespace Authentication.ApplicationServices.Identities
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

        private readonly IResendConfirmIdentityCommand _resendConfirmIdentityCommand;

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
            IResendConfirmIdentityCommand resendConfirmIdentityCommand,
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
            _resendConfirmIdentityCommand = resendConfirmIdentityCommand;
        }

        public async Task<IdentityAdto> GetAsync(GetIdentityAdto getIdentityAdto)
        {
            using (ITransaction transaction = _transactionManager.Create())
            {
                Identity identity = await _identityQueryRepository.GetByIdAsync(getIdentityAdto.Id);

                if (identity == null)
                {
                    throw new BusinessApplicationException(ExceptionType.NotFound, "Identity does not exist");
                }

                transaction.Commit();

                if (identity.HasPassword)
                {
                    return new PasswordIdentityAdto
                    {
                        Id = identity.Id,
                        Version = ConcurrencyVersionFactory.CreateFromEntity(identity)
                    };
                }

                return new IdentityAdto
                {
                    Id = identity.Id,
                    Version = ConcurrencyVersionFactory.CreateFromEntity(identity)
                };
            }
        }

        public async Task ResetPasswordAsync(ResetPasswordAdto resetPasswordAdto)
        {
            using (ITransaction transaction = _transactionManager.Create())
            {
                try
                {
                    Identity identity = await _resetPasswordCommand.ExecuteAsync(new ResetPasswordCommandDdto
                    {
                        Token = resetPasswordAdto.Token,
                        Password = resetPasswordAdto.Password,
                        ConfirmPassword = resetPasswordAdto.ConfirmPassword
                    });

                    await _identityCommandRepository.UpdateAsync(identity);

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

        public async Task ForgotPasswordAsync(ForgotPasswordAdto forgotPasswordAdto)
        {
            using (ITransaction transaction = _transactionManager.Create())
            {
                try
                {
                    Identity identity = await _forgotPasswordCommand.ExecuteAsync(new ForgotPasswordCommandDdto
                    {
                        EmailAddress = forgotPasswordAdto.EmailAddress
                    });

                    if (identity == null)
                    {
                        return;
                    }

                    await _identityCommandRepository.UpdateAsync(identity);

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

        public async Task ConfirmIdentityAsync(ConfirmIdentityAdto confirmIdentityAdto)
        {
            using (ITransaction transaction = _transactionManager.Create())
            {
                try
                {
                    Identity identity = await _identityCommandRepository.GetByIdAsync(confirmIdentityAdto.IdentityId);

                    if (identity == null)
                    {
                        throw new BusinessApplicationException(ExceptionType.NotFound, "No identity found to confirm identity");
                    }

                    await _confirmIdentityCommand.ExecuteAsync(identity, new ConfirmIdentityCommandDdto
                    {
                        Token = confirmIdentityAdto.Token
                    });

                    await _identityCommandRepository.UpdateAsync(identity);

                    transaction.Commit();
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

        public async Task<PasswordAdto> ChangePasswordAsync(ChangePasswordAdto changePasswordAdto)
        {
            using (ITransaction transaction = _transactionManager.Create())
            {
                try
                {
                    Identity identity = await _identityCommandRepository.GetWithConcurrencyCheckAsync(changePasswordAdto.IdentityId, changePasswordAdto.Version);

                    if (identity == null)
                    {
                        throw new BusinessApplicationException(ExceptionType.NotFound, "Identity does not exist");
                    }

                    await _changePasswordCommand.ExecuteAsync(identity, new ChangePasswordCommandDdto
                    {
                        Password = changePasswordAdto.Password,
                        ConfirmPassword = changePasswordAdto.ConfirmPassword
                    });

                    await _identityCommandRepository.UpdateAsync(identity);

                    transaction.Commit();

                    return new PasswordAdto
                    {
                        IdentityId = identity.Id
                    };
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
                catch (NotFoundDomainException e)
                {
                    throw new BusinessApplicationException(ExceptionType.NotFound, e);
                }
            }
        }

        public async Task<PasswordAdto> RegisterPasswordAsync(RegisterPasswordAdto registerPasswordAdto)
        {
            using (ITransaction transaction = _transactionManager.Create())
            {
                try
                {
                    Identity identity = await _createIdentityCommand.ExecuteAsync();

                    await _registerPasswordCommand.ExecuteAsync(identity, await GetAuthenticationGrantTypePasswordAsync(), new RegisterPasswordCommandDdto
                    {
                        Identifier = registerPasswordAdto.Identifier,
                        Password = registerPasswordAdto.Password,
                        ConfirmPassword = registerPasswordAdto.ConfirmPassword,
                        EmailAddress = registerPasswordAdto.EmailAddress
                    });

                    await _identityCommandRepository.AddAsync(identity);

                    transaction.Commit();

                    return new PasswordAdto
                    {
                        IdentityId = identity.Id
                    };
                }
                catch (DomainValidationRuleException e)
                {
                    throw new BusinessValidationRuleApplicationException(e.ValidationResult);
                }
            }
        }

        public async Task<RefreshTokenIdentityAdto> CreateRefreshTokenAsync(CreateRefreshTokenAdto createRefreshTokenAdto)
        {
            using (ITransaction transaction = _transactionManager.Create())
            {
                try
                {
                    Identity identity = await _identityCommandRepository.GetByIdAsync(createRefreshTokenAdto.IdentityId);

                    if (identity == null)
                    {
                        throw new BusinessApplicationException(ExceptionType.NotFound, "Identity not found");
                    }

                    AuthenticationGrantTypeRefreshToken authenticationGrantTypeRefreshToken = (AuthenticationGrantTypeRefreshToken) await
                        _authenticationServiceCommandRepository.GetSingleAsync(a => a is AuthenticationGrantTypeRefreshToken);

                    if (authenticationGrantTypeRefreshToken == null)
                    {
                        throw new BusinessApplicationException(ExceptionType.NotFound, "Refresh tokens are not configured");
                    }

                    RefreshTokenIdentityDdto refreshTokenIdentityDdto = await _createRefreshTokenCommand.ExecuteAsync(identity, authenticationGrantTypeRefreshToken);

                    await _identityCommandRepository.UpdateAsync(identity);

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

        public async Task ResendConfirmIdentityAsync(ResendConfirmIdentityAdto resendConfirmIdentityAdto)
        {
            using (ITransaction transaction = _transactionManager.Create())
            {
                try
                {
                    Identity identity = await _identityCommandRepository.GetByIdAsync(resendConfirmIdentityAdto.IdentityId);

                    if (identity == null)
                    {
                        throw new BusinessApplicationException(ExceptionType.Unauthorized, "No identity exists");
                    }

                    await _resendConfirmIdentityCommand.ExecuteAsync(identity);

                    await _identityCommandRepository.UpdateAsync(identity);

                    transaction.Commit();
                }
                catch (IdentityAlreadyConfirmedDomainException)
                {
                    throw new BusinessApplicationException(ExceptionType.BadRequest, ErrorCodes.IdentityAlreadyConfirmed, "Identity already confirmed");
                }
            }
        }

        public async Task LogoutAsync(LogoutAdto logoutAdto)
        {
            using (ITransaction transaction = _transactionManager.Create())
            {
                Identity identity = await _identityCommandRepository.GetByIdAsync(logoutAdto.IdentityId);

                if (identity == null)
                {
                    throw new BusinessApplicationException(ExceptionType.NotFound, "Identity does not exist");
                }

                await _logoutCommand.ExecuteAsync(identity);

                await _identityCommandRepository.UpdateAsync(identity);

                transaction.Commit();
            }
        }

        private async Task<AuthenticationGrantTypePassword> GetAuthenticationGrantTypePasswordAsync()
        {
            AuthenticationService authenticationService = await _authenticationServiceCommandRepository.GetSingleAsync(s => s is AuthenticationGrantTypePassword);

            if (!(authenticationService is AuthenticationGrantTypePassword authenticationGrantTypePassword))
            {
                throw new BusinessApplicationException(ExceptionType.BadRequest, ErrorCodes.PasswordLoginNotConfigured, "Password logins are not configured");
            }

            return authenticationGrantTypePassword;
        }
    }
}
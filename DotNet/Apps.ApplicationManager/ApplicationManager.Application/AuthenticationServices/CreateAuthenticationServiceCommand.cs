using System;
using ApplicationManager.ApplicationServices.AuthenticationServices.Interfaces;
using ApplicationManager.ApplicationServices.AuthenticationServices.Models;
using ApplicationManager.Domain.AuthenticationServices;
using Common.Application.Transactions;
using Common.ApplicationServices.Services.Command;

namespace ApplicationManager.ApplicationServices.AuthenticationServices
{
    public class CreateAuthenticationServiceCommand : ICreateAuthenticationServiceCommand
    {
        private readonly ITransactionManager _transactionManager;

        private readonly ICommandService<AuthenticationService> _commandService;

        public CreateAuthenticationServiceCommand(ITransactionManager transactionManager,
            ICommandService<AuthenticationService> commandService)
        {
            _transactionManager = transactionManager;
            _commandService = commandService;
        }

        public Guid Everyone()
        {
            using (ITransaction transaction = _transactionManager.Create())
            {
                AuthenticationGrantTypeRefreshToken authenticationGrantTypeEveryone =
                    AuthenticationGrantTypeRefreshToken.Create();

                _commandService.Create(authenticationGrantTypeEveryone);

                transaction.Commit();

                return authenticationGrantTypeEveryone.Id;
            }
        }

        public Guid Password()
        {
            using (ITransaction transaction = _transactionManager.Create())
            {
                AuthenticationGrantTypePassword authenticationGrantTypePassword =
                    AuthenticationGrantTypePassword.Create();

                _commandService.Create(authenticationGrantTypePassword);

                transaction.Commit();

                return authenticationGrantTypePassword.Id;
            }
        }

        public Guid ClientCredential(
            CreateAuthenticationGrantTypeClientCredentialAdto createAuthenticationGrantTypeClientCredentialAdto)
        {
            using (ITransaction transaction = _transactionManager.Create())
            {
                AuthenticationGrantTypeClientCredential authenticationGrantTypeClientCredential =
                    AuthenticationGrantTypeClientCredential.Create(new CreateAuthenticationGrantTypeClientCredentialDdto
                    {
                        Name = createAuthenticationGrantTypeClientCredentialAdto.Name,
                        ClientId = createAuthenticationGrantTypeClientCredentialAdto.ClientId,
                        ClientSecret = createAuthenticationGrantTypeClientCredentialAdto.ClientSecret,
                        ClientGrantAccessTokenUrl = createAuthenticationGrantTypeClientCredentialAdto.ClientGrantAccessTokenUrl,
                        GrantAccessTokenUrl = createAuthenticationGrantTypeClientCredentialAdto.GrantAccessTokenUrl,
                        ValidateAccessTokenUrl = createAuthenticationGrantTypeClientCredentialAdto.ValidateAccessTokenUrl
                    });

                _commandService.Create(authenticationGrantTypeClientCredential);

                transaction.Commit();

                return authenticationGrantTypeClientCredential.Id;
            }
        }
    }
}

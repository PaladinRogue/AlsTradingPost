using System;
using ApplicationManager.ApplicationServices.AuthenticationServices.Interfaces;
using ApplicationManager.ApplicationServices.AuthenticationServices.Models;
using ApplicationManager.Domain.AuthenticationServices;
using Common.Application.Transactions;
using Common.Domain.Persistence;

namespace ApplicationManager.ApplicationServices.AuthenticationServices
{
    public class CreateAuthenticationServiceCommand : ICreateAuthenticationServiceCommand
    {
        private readonly ITransactionManager _transactionManager;

        private readonly ICreateAuthenticationGrantTypeClientCredentialCommand _createAuthenticationGrantTypeClientCredentialCommand;

        private readonly ICommandRepository<AuthenticationService> _commandRepository;

        public CreateAuthenticationServiceCommand(
            ITransactionManager transactionManager,
            ICreateAuthenticationGrantTypeClientCredentialCommand createAuthenticationGrantTypeClientCredentialCommand,
            ICommandRepository<AuthenticationService> commandRepository)
        {
            _transactionManager = transactionManager;
            _createAuthenticationGrantTypeClientCredentialCommand = createAuthenticationGrantTypeClientCredentialCommand;
            _commandRepository = commandRepository;
        }

        public Guid ClientCredential(
            CreateAuthenticationGrantTypeClientCredentialAdto createAuthenticationGrantTypeClientCredentialAdto)
        {
            using (ITransaction transaction = _transactionManager.Create())
            {
                AuthenticationGrantTypeClientCredential authenticationGrantTypeClientCredential =
                    _createAuthenticationGrantTypeClientCredentialCommand.Execute(new CreateAuthenticationGrantTypeClientCredentialDdto
                    {
                        Name = createAuthenticationGrantTypeClientCredentialAdto.Name,
                        ClientId = createAuthenticationGrantTypeClientCredentialAdto.ClientId,
                        ClientSecret = createAuthenticationGrantTypeClientCredentialAdto.ClientSecret,
                        ClientGrantAccessTokenUrl = createAuthenticationGrantTypeClientCredentialAdto.ClientGrantAccessTokenUrl,
                        GrantAccessTokenUrl = createAuthenticationGrantTypeClientCredentialAdto.GrantAccessTokenUrl,
                        ValidateAccessTokenUrl = createAuthenticationGrantTypeClientCredentialAdto.ValidateAccessTokenUrl
                    });

                _commandRepository.Add(authenticationGrantTypeClientCredential);

                transaction.Commit();

                return authenticationGrantTypeClientCredential.Id;
            }
        }
    }
}

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

        private readonly IRepository<AuthenticationService> _repository;

        public CreateAuthenticationServiceCommand(ITransactionManager transactionManager,
            IRepository<AuthenticationService> repository)
        {
            _transactionManager = transactionManager;
            _repository = repository;
        }

        public Guid Everyone()
        {
            using (ITransaction transaction = _transactionManager.Create())
            {
                AuthenticationGrantTypeRefreshToken authenticationGrantTypeEveryone =
                    AuthenticationGrantTypeRefreshToken.Create();

                _repository.Add(authenticationGrantTypeEveryone);

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

                _repository.Add(authenticationGrantTypePassword);

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

                _repository.Add(authenticationGrantTypeClientCredential);

                transaction.Commit();

                return authenticationGrantTypeClientCredential.Id;
            }
        }
    }
}

using System;
using ApplicationManager.ApplicationServices.AuthenticationServices.Models;

namespace ApplicationManager.ApplicationServices.AuthenticationServices.Interfaces
{
    public interface ICreateAuthenticationServiceCommand
    {
        Guid ClientCredential(CreateAuthenticationGrantTypeClientCredentialAdto createAuthenticationGrantTypeClientCredentialAdto);
    }
}
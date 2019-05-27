using ApplicationManager.Domain.AuthenticationServices;
using ApplicationManager.Domain.Identities.AuthenticationIdentities;

namespace ApplicationManager.Domain.Identities
{
    public interface ICreateTwoFactorAuthenticationIdentityCommand
    {
        void Execute(
            Identity identity,
            CreateTwoFactorAuthenticationIdentityDdto createTwoFactorAuthenticationIdentityDdto);
    }
}
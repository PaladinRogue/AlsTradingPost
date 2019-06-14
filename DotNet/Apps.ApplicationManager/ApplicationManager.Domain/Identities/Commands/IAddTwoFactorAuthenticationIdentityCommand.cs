using ApplicationManager.Domain.Identities.Models;

namespace ApplicationManager.Domain.Identities.Commands
{
    public interface IAddTwoFactorAuthenticationIdentityCommand
    {
        void Execute(
            Identity identity,
            AddTwoFactorAuthenticationIdentityDdto addTwoFactorAuthenticationIdentityDdto);
    }
}
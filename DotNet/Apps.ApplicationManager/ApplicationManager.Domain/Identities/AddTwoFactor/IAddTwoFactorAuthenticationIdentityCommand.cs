namespace ApplicationManager.Domain.Identities.AddTwoFactor
{
    public interface IAddTwoFactorAuthenticationIdentityCommand
    {
        void Execute(
            Identity identity,
            AddTwoFactorAuthenticationIdentityDdto addTwoFactorAuthenticationIdentityDdto);
    }
}
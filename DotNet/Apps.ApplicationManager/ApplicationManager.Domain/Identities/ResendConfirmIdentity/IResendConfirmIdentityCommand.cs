namespace ApplicationManager.Domain.Identities.ResendConfirmIdentity
{
    public interface IResendConfirmIdentityCommand
    {
        void Execute(Identity identity);
    }
}
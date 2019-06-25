namespace ApplicationManager.Domain.Identities.ConfirmIdentity
{
    public interface IConfirmIdentityCommand
    {
        void Execute(
            Identity identity,
            ConfirmIdentityCommandDdto confirmIdentityCommandDdto);
    }
}
namespace ApplicationManager.Domain.Identities.ResendConfirmIdentity
{
    public class ResendConfirmIdentityCommand : IResendConfirmIdentityCommand
    {
        public void Execute(Identity identity)
        {
            identity.ResendConfirmIdentity();
        }
    }
}
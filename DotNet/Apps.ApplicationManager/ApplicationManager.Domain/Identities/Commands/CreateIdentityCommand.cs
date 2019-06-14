namespace ApplicationManager.Domain.Identities.Commands
{
    public class CreateIdentityCommand : ICreateIdentityCommand
    {
        public Identity Execute()
        {
            return Identity.Create();
        }
    }
}

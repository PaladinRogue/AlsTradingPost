namespace ApplicationManager.Domain.Identities
{
    public class CreateIdentityCommand : ICreateIdentityCommand
    {
        public Identity Execute()
        {
            return Identity.Create();
        }
    }
}

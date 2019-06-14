namespace ApplicationManager.Domain.Identities.Create
{
    public class CreateIdentityCommand : ICreateIdentityCommand
    {
        public Identity Execute()
        {
            return Identity.Create();
        }
    }
}

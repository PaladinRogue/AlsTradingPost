namespace ApplicationManager.Domain.Identities.Create
{
    public class CreateIdentityCommand : ICreateIdentityCommand
    {
        public Identity Execute(CreateIdentityCommandDdto createIdentityCommandDdto)
        {
            return Identity.Create(new CreateIdentityDdto
            {
                EmailAddress = createIdentityCommandDdto.EmailAddress
            });
        }
    }
}

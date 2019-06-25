namespace ApplicationManager.Domain.Identities.Create
{
    public interface ICreateIdentityCommand
    {
        Identity Execute(CreateIdentityCommandDdto createIdentityCommandDdto);
    }
}

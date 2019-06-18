namespace ApplicationManager.Domain.Identities.Queries
{
    public interface IGetIdentityByIdentifierAndPasswordQuery
    {
        Identity Run(string identifier, string password);
    }
}
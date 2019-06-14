namespace ApplicationManager.Domain.Identities.Queries
{
    public interface IPasswordIdentityIdentifierIsUniqueQuery
    {
        bool Run(string identifier);
    }
}
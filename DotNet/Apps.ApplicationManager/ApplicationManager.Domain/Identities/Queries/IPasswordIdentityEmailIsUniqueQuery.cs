namespace ApplicationManager.Domain.Identities.Queries
{
    public interface IPasswordIdentityEmailIsUniqueQuery
    {
        bool Run(string emailAddress);
    }
}
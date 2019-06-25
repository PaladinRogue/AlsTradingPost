namespace ApplicationManager.Domain.Identities.Queries
{
    public interface IGetIdentityByEmailAddressQuery
    {
        Identity Run(string emailAddress);
    }
}
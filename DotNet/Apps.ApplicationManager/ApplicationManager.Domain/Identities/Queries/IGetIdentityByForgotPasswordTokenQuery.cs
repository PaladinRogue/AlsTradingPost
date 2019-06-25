namespace ApplicationManager.Domain.Identities.Queries
{
    public interface IGetIdentityByForgotPasswordTokenQuery
    {
        Identity Run(string token);
    }
}
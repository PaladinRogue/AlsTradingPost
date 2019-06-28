namespace ApplicationManager.Domain.Identities.Login.RefreshToken
{
    public interface IRefreshTokenLoginCommand
    {
        Identity Execute(RefreshTokenLoginCommandDdto refreshTokenLoginCommandDdto);
    }
}
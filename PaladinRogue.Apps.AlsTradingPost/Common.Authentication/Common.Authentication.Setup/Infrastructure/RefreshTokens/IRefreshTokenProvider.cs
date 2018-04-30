namespace Common.Authentication.Setup.Infrastructure.RefreshTokens
{
    public interface IRefreshTokenProvider
    {
        string GenerateRefreshToken<T>(T data);
    }
}
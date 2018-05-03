namespace Common.Authentication.Resources.RefreshTokens
{
    public interface IRefreshTokenProvider
    {
        string GenerateRefreshToken<T>(T data);
    }
}
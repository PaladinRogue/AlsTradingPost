namespace Common.Api.Authentication.Interfaces
{
    public interface IJwtResource
    {
        string AuthToken { get; set; }
        int ExpiresIn { get; set; }
    }
}

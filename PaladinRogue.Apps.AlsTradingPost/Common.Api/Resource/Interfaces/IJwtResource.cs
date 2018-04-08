namespace Common.Api.Resource.Interfaces
{
    public interface IJwtResource
    {
        string AuthToken { get; set; }
        int ExpiresIn { get; set; }
    }
}

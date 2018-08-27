using Common.Api.Resources;

namespace Common.Api.Authentication.Interfaces
{
    public interface IJwtResource : IResource
    {
        string AuthToken { get; set; }
        int ExpiresIn { get; set; }
    }
}

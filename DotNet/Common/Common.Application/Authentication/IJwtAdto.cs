using System;

namespace Common.Application.Authentication
{
    public interface IJwtAdto
    {
        string AuthToken { get; set; }
        int ExpiresIn { get; set; }
        string RefreshToken { get; set; }
        Guid SessionId { get; set; }
    }
}
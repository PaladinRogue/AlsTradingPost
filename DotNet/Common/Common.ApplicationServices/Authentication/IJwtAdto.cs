using System;

namespace Common.ApplicationServices.Authentication
{
    public interface IJwtAdto
    {
        string AuthToken { get; set; }

        int ExpiresIn { get; set; }

        Guid SessionId { get; set; }
    }
}
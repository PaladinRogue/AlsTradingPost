using System;

namespace PaladinRogue.Libray.Core.Application.Authentication
{
    public interface IJwtAdto
    {
        string AuthToken { get; set; }

        int ExpiresIn { get; set; }

        Guid SessionId { get; set; }
    }
}
using System;

namespace PaladinRogue.Libray.Authorisation.Setup.Infrastructure.Restrictions
{
    public class AuthorisationRestrictionNotFoundException : Exception
    {
        public AuthorisationRestrictionNotFoundException(string message) : base(message)
        {
        }
    }
}
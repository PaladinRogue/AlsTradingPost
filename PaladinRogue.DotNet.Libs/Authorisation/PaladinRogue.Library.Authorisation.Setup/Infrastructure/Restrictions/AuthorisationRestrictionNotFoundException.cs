using System;

namespace PaladinRogue.Library.Authorisation.Setup.Infrastructure.Restrictions
{
    public class AuthorisationRestrictionNotFoundException : Exception
    {
        public AuthorisationRestrictionNotFoundException(string message) : base(message)
        {
        }
    }
}
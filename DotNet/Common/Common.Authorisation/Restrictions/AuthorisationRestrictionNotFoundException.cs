using System;

namespace Common.Authorisation.Restrictions
{
    public class AuthorisationRestrictionNotFoundException : Exception
    {
        public AuthorisationRestrictionNotFoundException(string message) : base(message)
        {
        }
    }
}
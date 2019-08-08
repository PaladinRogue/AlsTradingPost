using System;

namespace Authorisation.Application.Restrictions
{
    public class AuthorisationRestrictionNotFoundException : Exception
    {
        public AuthorisationRestrictionNotFoundException(string message) : base(message)
        {
        }
    }
}
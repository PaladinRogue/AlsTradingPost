using System;

namespace Common.Application.Authorisation
{
    [Serializable]
    public class AuthorisationDeniedException : Exception
    {
        public AuthorisationDeniedException()
        {
        }

        public AuthorisationDeniedException(string message) : base(message)
        {
        }

        public AuthorisationDeniedException(string message, Exception innerException) : base(message, innerException)
        {
        }
    }
}

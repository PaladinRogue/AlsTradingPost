using System;

namespace ApplicationManager.Domain.AuthenticationServices
{
    public class AuthenticationGrantTypePassword : AuthenticationService
    {
        public override string Type
        {
            get => AuthenticationGrantTypes.Password;
            protected set => throw new NotSupportedException();
        }

        protected AuthenticationGrantTypePassword()
        {
        }

        internal static AuthenticationGrantTypePassword Create()
        {
            return new AuthenticationGrantTypePassword();
        }
    }
}

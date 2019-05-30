using System;

namespace ApplicationManager.Domain.AuthenticationServices
{
    public class AuthenticationGrantTypeRefreshToken : AuthenticationService
    {
        public override string Type
        {
            get => AuthenticationGrantTypes.RefreshToken;
            protected set => throw new NotSupportedException();
        }

        protected AuthenticationGrantTypeRefreshToken()
        {
        }

        internal static AuthenticationGrantTypeRefreshToken Create()
        {
            return new AuthenticationGrantTypeRefreshToken();
        }
    }
}

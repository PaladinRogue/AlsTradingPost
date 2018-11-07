using System;

namespace ApplicationManager.Domain.AuthenticationServices
{
    public class AuthenticationGrantTypeRefreshToken : AuthenticationService
    {
        public override string Type
        {
            get => "REFRESH_TOKEN";
            protected set => throw new NotSupportedException();
        }

        protected AuthenticationGrantTypeRefreshToken()
        {
        }

        public static AuthenticationGrantTypeRefreshToken Create()
        {
            return new AuthenticationGrantTypeRefreshToken();
        }
    }
}

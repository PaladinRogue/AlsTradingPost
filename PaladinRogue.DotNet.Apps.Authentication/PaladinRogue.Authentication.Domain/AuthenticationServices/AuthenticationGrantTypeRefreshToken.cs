﻿namespace PaladinRogue.Authentication.Domain.AuthenticationServices
{
    public class AuthenticationGrantTypeRefreshToken : AuthenticationService
    {
        protected AuthenticationGrantTypeRefreshToken()
        {
        }

        internal static AuthenticationGrantTypeRefreshToken Create()
        {
            return new AuthenticationGrantTypeRefreshToken();
        }
    }
}

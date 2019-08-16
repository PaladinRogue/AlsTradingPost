namespace PaladinRogue.Authentication.Domain.AuthenticationServices
{
    public class AuthenticationGrantTypePassword : AuthenticationService
    {
        protected AuthenticationGrantTypePassword()
        {
        }

        internal static AuthenticationGrantTypePassword Create()
        {
            return new AuthenticationGrantTypePassword();
        }
    }
}

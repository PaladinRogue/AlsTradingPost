namespace ApplicationManager.Domain.AuthenticationServices
{
    public class AuthenticationGrantTypePassword : AuthenticationService
    {
        public override string TypeDiscriminator => "Password";
    }
}

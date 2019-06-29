namespace ApplicationManager.Domain.AuthenticationServices.ChangeClientCredential
{
    public interface IChangeAuthenticationGrantTypeClientCredentialCommand
    {
        void Execute(
            AuthenticationGrantTypeClientCredential authenticationGrantTypeClientCredential,
            ChangeAuthenticationGrantTypeClientCredentialDdto changeAuthenticationGrantTypeClientCredentialDdto);
    }
}
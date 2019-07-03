using ApplicationManager.ApplicationServices.Authentication.ClientCredential;

namespace ApplicationManager.Setup.Infrastructure.Authentication.ClientCredential
{
    public class ClientCredentialAuthenticationResult : IClientCredentialAuthenticationResult
    {
        public bool Success { get; set; }

        public string Identifier { get; set; }

        public static IClientCredentialAuthenticationResult Succeed(string identifier)
        {
            return new ClientCredentialAuthenticationResult
            {
                Identifier = identifier,
                Success = true
            };
        }

        public static IClientCredentialAuthenticationResult Fail()
        {
            return new ClientCredentialAuthenticationResult
            {
                Success = false
            };
        }
    }
}
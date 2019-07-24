using Authentication.ApplicationServices.Authentication.ClientCredential;

namespace Authentication.Setup.Infrastructure.Authentication.ClientCredential
{
    public class ClientCredentialAuthenticationResult : IClientCredentialAuthenticationResult
    {
        protected ClientCredentialAuthenticationResult()
        {
        }

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

        public static IClientCredentialAuthenticationResult Fail =>
            new ClientCredentialAuthenticationResult
            {
                Success = false
            };
    }
}
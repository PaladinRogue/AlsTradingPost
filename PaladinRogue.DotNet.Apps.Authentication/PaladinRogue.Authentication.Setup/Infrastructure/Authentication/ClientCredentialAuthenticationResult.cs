using PaladinRogue.Authentication.Application.Authentication.ClientCredential;

namespace PaladinRogue.Authentication.Setup.Infrastructure.Authentication
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
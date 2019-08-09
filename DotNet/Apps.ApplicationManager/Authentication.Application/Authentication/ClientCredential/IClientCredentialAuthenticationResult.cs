namespace Authentication.Application.Authentication.ClientCredential
{
    public interface IClientCredentialAuthenticationResult
    {
        bool Success { get; set; }

        string Identifier { get; set; }
    }
}
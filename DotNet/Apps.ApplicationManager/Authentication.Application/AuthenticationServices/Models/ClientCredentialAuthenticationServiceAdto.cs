namespace Authentication.Application.AuthenticationServices.Models
{
    public class ClientCredentialAuthenticationServiceAdto : AuthenticationServiceAdto
    {
        public string AccessUrl { get; set; }

        public string Name { get; set; }
    }
}
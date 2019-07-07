namespace ApplicationManager.Domain.AuthenticationServices.ChangeClientCredential
{
    public class ChangeAuthenticationGrantTypeClientCredentialDdto
    {
        public string Name { get; set; }

        public string ClientId { get; set; }

        public string ClientSecret { get; set; }

        public string ClientGrantAccessTokenUrl { get; set; }

        public string GrantAccessTokenUrl { get; set; }

        public string ValidateAccessTokenUrl { get; set; }

        public string AppAccessToken { get; set; }
    }
}
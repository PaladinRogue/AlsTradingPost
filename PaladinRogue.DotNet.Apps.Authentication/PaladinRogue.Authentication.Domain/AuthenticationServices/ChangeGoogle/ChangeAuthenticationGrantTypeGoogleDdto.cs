namespace PaladinRogue.Authentication.Domain.AuthenticationServices.ChangeGoogle
{
    public class ChangeAuthenticationGrantTypeGoogleDdto
    {
        public string Name { get; set; }

        public string ClientId { get; set; }

        public string ClientSecret { get; set; }

        public string ClientGrantAccessTokenUrl { get; set; }

        public string GrantAccessTokenUrl { get; set; }

        public string ValidateAccessTokenUrl { get; set; }
    }
}
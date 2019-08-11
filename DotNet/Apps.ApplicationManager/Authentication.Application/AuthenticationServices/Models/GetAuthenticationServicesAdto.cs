namespace PaladinRogue.Authentication.Application.AuthenticationServices.Models
{
    public class GetAuthenticationServicesAdto
    {
        public string RedirectUri { get; set; }

        public string State { get; set; }
    }
}
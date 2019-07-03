namespace ApplicationManager.ApplicationServices.Authentication.Models
{
    public class ClientCredentialAdto
    {
        public string State { get; set; }

        public string RedirectUri { get; set; }

        public string Token { get; set; }
    }
}
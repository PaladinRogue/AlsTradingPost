namespace Authentication.ApplicationServices.AuthenticationServices.Models.Facebook
{
    public class CreateFacebookAdto : CreateClientCredentialAdto
    {
        public string AppAccessToken { get; set; }
    }
}
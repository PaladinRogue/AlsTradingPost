namespace Authentication.Application.AuthenticationServices.Models.Facebook
{
    public class CreateFacebookAdto : CreateClientCredentialAdto
    {
        public string AppAccessToken { get; set; }
    }
}
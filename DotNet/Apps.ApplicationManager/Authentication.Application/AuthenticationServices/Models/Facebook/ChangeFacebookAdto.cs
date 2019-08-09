namespace Authentication.Application.AuthenticationServices.Models.Facebook
{
    public class ChangeFacebookAdto : ChangeClientCredentialAdto
    {
        public string AppAccessToken { get; set; }
    }
}
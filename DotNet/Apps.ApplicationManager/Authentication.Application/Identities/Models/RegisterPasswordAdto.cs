namespace Authentication.Application.Identities.Models
{
    public class RegisterPasswordAdto
    {
        public string Identifier { get; set; }

        public string Password { get; set; }

        public string ConfirmPassword { get; set; }

        public string EmailAddress { get; set; }
    }
}
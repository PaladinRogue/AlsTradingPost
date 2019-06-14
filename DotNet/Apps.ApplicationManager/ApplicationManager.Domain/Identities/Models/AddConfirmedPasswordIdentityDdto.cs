namespace ApplicationManager.Domain.Identities.Models
{
    public class AddConfirmedPasswordIdentityDdto
    {
        public string Token { get; set; }

        public string Identifier { get; set; }

        public string Password { get; set; }

        public string ConfirmPassword { get; set; }
    }
}
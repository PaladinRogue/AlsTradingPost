namespace ApplicationManager.Domain.Identities.AuthenticationIdentities
{
    public class CreatePasswordIdentityDdto
    {
        public string Identifier { get; set; }

        public string Password { get; set; }

        public string ConfirmPassword { get; set; }
    }
}
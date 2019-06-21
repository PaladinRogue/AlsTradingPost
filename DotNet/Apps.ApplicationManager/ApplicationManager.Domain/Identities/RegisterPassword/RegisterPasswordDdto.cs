namespace ApplicationManager.Domain.Identities.RegisterPassword
{
    public class RegisterPasswordDdto
    {
        public string Identifier { get; set; }

        public string Password { get; set; }

        public string ConfirmPassword { get; set; }

        public string EmailAddress { get; set; }
    }
}
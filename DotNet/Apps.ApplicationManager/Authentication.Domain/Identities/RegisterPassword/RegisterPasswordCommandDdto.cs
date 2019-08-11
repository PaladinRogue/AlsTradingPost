namespace PaladinRogue.Authentication.Domain.Identities.RegisterPassword
{
    public class RegisterPasswordCommandDdto
    {
        public string Identifier { get; set; }

        public string Password { get; set; }

        public string ConfirmPassword { get; set; }

        public string EmailAddress { get; set; }
    }
}
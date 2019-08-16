namespace PaladinRogue.Authentication.Domain.Identities.ResetPassword
{
    public class ResetPasswordCommandDdto
    {
        public string Token { get; set; }

        public string Password { get; set; }

        public string ConfirmPassword { get; set; }
    }
}
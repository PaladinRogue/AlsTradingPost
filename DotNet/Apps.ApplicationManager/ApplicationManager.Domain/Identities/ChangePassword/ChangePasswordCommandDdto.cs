namespace ApplicationManager.Domain.Identities.ChangePassword
{
    public class ChangePasswordCommandDdto
    {
        public string Password { get; set; }

        public string ConfirmPassword { get; set; }
    }
}
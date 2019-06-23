namespace ApplicationManager.Domain.Identities.CreateTwoFactor
{
    internal class CreateTwoFactorAuthenticationIdentityDdto
    {
        public string EmailAddress { get; set; }

        public string TwoFactorAuthenticationType { get; set; }
    }
}
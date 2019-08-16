namespace PaladinRogue.Authentication.Domain.Identities.CreateTwoFactor
{
    internal class CreateTwoFactorAuthenticationIdentityDdto
    {
        public string EmailAddress { get; set; }

        public TwoFactorAuthenticationType TwoFactorAuthenticationType { get; set; }
    }
}
namespace PaladinRogue.Authentication.Domain.Identities.ValidateToken
{
    internal class ValidateTokenDdto
    {
        public string Token { get; set; }

        public TwoFactorAuthenticationType TwoFactorAuthenticationType { get; set; }
    }
}
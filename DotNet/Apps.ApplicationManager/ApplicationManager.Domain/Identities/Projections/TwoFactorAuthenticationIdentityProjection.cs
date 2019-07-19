using Common.Domain.DataProtectors;

namespace ApplicationManager.Domain.Identities.Projections
{
    public class TwoFactorAuthenticationIdentityProjection
    {
        [SensitiveInformation]
        public string EmailAddress { get; set; }
    }
}
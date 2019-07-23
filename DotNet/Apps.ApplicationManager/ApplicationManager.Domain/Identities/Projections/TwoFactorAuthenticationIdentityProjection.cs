using Common.Domain.DataProtectors;

namespace ApplicationManager.Domain.Identities.Projections
{
    public class TwoFactorAuthenticationIdentityProjection
    {
        [SensitiveInformation(DataKeys.EmailAddress)]
        public string EmailAddress { get; set; }
    }
}
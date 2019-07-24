using Common.Domain.DataProtectors;

namespace Authentication.Domain.Identities.Projections
{
    public class TwoFactorAuthenticationIdentityProjection
    {
        [SensitiveInformation(DataKeys.EmailAddress)]
        public string EmailAddress { get; set; }
    }
}
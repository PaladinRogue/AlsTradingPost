using PaladinRogue.Library.Core.Domain.DataProtectors;

namespace PaladinRogue.Authentication.Domain.Identities.Projections
{
    public class TwoFactorAuthenticationIdentityProjection
    {
        [SensitiveInformation(DataKeys.EmailAddress)]
        public string EmailAddress { get; set; }
    }
}
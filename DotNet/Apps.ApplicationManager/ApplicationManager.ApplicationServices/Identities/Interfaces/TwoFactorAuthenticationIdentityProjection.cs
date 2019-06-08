using Common.Domain.Models.DataProtection;

namespace ApplicationManager.ApplicationServices.Identities.Interfaces
{
    public class TwoFactorAuthenticationIdentityProjection
    {
        [SensitiveInformation]
        public string EmailAddress { get; set; }

        [SensitiveInformation]
        public string Token { get; set; }
    }
}
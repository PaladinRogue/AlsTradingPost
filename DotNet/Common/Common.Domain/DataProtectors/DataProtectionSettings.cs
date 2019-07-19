using Microsoft.IdentityModel.Tokens;

namespace Common.Domain.DataProtectors
{
    public class DataProtectionSettings
    {
        public string Secret { get; set; }

        public SymmetricSecurityKey SigningKey { get; set; }
    }
}
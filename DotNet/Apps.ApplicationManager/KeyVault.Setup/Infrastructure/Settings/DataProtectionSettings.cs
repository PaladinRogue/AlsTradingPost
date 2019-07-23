using Microsoft.IdentityModel.Tokens;

namespace KeyVault.Setup.Infrastructure.Settings
{
    public class DataProtectionSettings
    {
        public string MasterKey { get; set; }

        public SymmetricSecurityKey MasterSecurityKey { get; set; }
    }
}
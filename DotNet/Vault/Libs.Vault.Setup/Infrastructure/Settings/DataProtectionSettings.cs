using Microsoft.IdentityModel.Tokens;

namespace PaladinRogue.Libray.Vault.Setup.Infrastructure.Settings
{
    public class DataProtectionSettings
    {
        public string MasterKey { get; set; }

        public SymmetricSecurityKey MasterSecurityKey { get; set; }
    }
}
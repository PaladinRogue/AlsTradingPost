using Microsoft.IdentityModel.Tokens;

namespace PaladinRogue.Libray.Vault.Domain.Applications.AddDataKey
{
    public class AddApplicationDataKeyCommandDdto
    {
        public string Type { get; set; }

        public SymmetricSecurityKey Value { get; set; }
    }
}
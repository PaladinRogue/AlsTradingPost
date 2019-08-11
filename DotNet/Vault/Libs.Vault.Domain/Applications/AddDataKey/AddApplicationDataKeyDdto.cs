using Microsoft.IdentityModel.Tokens;

namespace PaladinRogue.Libray.Vault.Domain.Applications.AddDataKey
{
    internal class AddApplicationDataKeyDdto
    {
        public string Type { get; set; }

        public SymmetricSecurityKey Value { get; set; }
    }
}
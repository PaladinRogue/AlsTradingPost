using Microsoft.IdentityModel.Tokens;

namespace PaladinRogue.Library.Vault.Domain.Applications.CreateDataKey
{
    internal class CreateDataKeyDdto
    {
        public string Type { get; set; }

        public SymmetricSecurityKey Value { get; set; }
    }
}
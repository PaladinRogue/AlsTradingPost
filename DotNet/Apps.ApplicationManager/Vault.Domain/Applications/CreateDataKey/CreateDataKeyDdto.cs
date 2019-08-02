using Microsoft.IdentityModel.Tokens;

namespace Vault.Domain.Applications.CreateDataKey
{
    internal class CreateDataKeyDdto
    {
        public string Type { get; set; }

        public SymmetricSecurityKey Value { get; set; }
    }
}
using Microsoft.IdentityModel.Tokens;

namespace KeyVault.Domain.Applications.CreateDataKey
{
    internal class CreateDataKeyDdto
    {
        public int Type { get; set; }

        public SymmetricSecurityKey Value { get; set; }
    }
}
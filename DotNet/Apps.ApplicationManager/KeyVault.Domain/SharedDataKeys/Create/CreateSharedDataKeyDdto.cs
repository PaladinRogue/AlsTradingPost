using Microsoft.IdentityModel.Tokens;

namespace KeyVault.Domain.SharedDataKeys.Create
{
    internal class CreateSharedDataKeyDdto
    {
        public SharedDatKeyType Type { get; set; }

        public SymmetricSecurityKey Value { get; set; }
    }
}
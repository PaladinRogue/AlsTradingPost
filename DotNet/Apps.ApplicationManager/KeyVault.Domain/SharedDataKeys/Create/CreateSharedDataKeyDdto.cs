using Microsoft.IdentityModel.Tokens;

namespace KeyVault.Domain.SharedDataKeys.Create
{
    internal class CreateSharedDataKeyDdto
    {
        public string Name { get; set; }

        public SymmetricSecurityKey Value { get; set; }
    }
}
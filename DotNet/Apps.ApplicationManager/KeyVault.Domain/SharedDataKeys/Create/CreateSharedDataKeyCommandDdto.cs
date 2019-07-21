using Microsoft.IdentityModel.Tokens;

namespace KeyVault.Domain.SharedDataKeys.Create
{
    public class CreateSharedDataKeyCommandDdto
    {
        public SharedDataKeyType Type { get; set; }

        public SymmetricSecurityKey Value { get; set; }
    }
}
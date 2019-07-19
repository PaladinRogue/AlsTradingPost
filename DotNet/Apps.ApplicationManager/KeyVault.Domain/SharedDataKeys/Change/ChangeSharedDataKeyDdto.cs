using Microsoft.IdentityModel.Tokens;

namespace KeyVault.Domain.SharedDataKeys.Change
{
    internal class ChangeSharedDataKeyDdto
    {
        public SymmetricSecurityKey Value { get; set; }
    }
}
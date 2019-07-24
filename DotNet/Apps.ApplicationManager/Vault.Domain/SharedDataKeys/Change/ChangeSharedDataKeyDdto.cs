using Microsoft.IdentityModel.Tokens;

namespace Vault.Domain.SharedDataKeys.Change
{
    internal class ChangeSharedDataKeyDdto
    {
        public SymmetricSecurityKey Value { get; set; }
    }
}
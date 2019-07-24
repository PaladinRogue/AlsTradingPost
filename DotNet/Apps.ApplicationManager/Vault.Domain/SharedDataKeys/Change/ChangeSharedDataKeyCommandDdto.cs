using Microsoft.IdentityModel.Tokens;

namespace Vault.Domain.SharedDataKeys.Change
{
    public class ChangeSharedDataKeyCommandDdto
    {
        public SymmetricSecurityKey Value { get; set; }
    }
}
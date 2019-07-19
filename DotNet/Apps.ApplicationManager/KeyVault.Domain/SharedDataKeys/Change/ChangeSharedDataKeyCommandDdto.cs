using Microsoft.IdentityModel.Tokens;

namespace KeyVault.Domain.SharedDataKeys.Change
{
    public class ChangeSharedDataKeyCommandDdto
    {
        public SymmetricSecurityKey Value { get; set; }
    }
}
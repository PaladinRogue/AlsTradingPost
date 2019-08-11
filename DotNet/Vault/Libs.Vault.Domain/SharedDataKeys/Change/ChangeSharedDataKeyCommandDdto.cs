using Microsoft.IdentityModel.Tokens;

namespace PaladinRogue.Libray.Vault.Domain.SharedDataKeys.Change
{
    public class ChangeSharedDataKeyCommandDdto
    {
        public SymmetricSecurityKey Value { get; set; }
    }
}
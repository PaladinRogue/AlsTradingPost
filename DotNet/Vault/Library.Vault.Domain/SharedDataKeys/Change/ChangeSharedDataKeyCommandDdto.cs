using Microsoft.IdentityModel.Tokens;

namespace PaladinRogue.Library.Vault.Domain.SharedDataKeys.Change
{
    public class ChangeSharedDataKeyCommandDdto
    {
        public SymmetricSecurityKey Value { get; set; }
    }
}
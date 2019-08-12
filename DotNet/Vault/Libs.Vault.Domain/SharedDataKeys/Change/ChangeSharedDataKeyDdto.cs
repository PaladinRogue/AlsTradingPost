using Microsoft.IdentityModel.Tokens;

namespace PaladinRogue.Library.Vault.Domain.SharedDataKeys.Change
{
    internal class ChangeSharedDataKeyDdto
    {
        public SymmetricSecurityKey Value { get; set; }
    }
}
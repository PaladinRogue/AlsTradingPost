using Microsoft.IdentityModel.Tokens;

namespace PaladinRogue.Libray.Vault.Domain.SharedDataKeys.Change
{
    internal class ChangeSharedDataKeyDdto
    {
        public SymmetricSecurityKey Value { get; set; }
    }
}
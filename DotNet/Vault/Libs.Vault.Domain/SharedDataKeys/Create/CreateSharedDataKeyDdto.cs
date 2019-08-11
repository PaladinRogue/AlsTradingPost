using Microsoft.IdentityModel.Tokens;

namespace PaladinRogue.Libray.Vault.Domain.SharedDataKeys.Create
{
    internal class CreateSharedDataKeyDdto
    {
        public string Name { get; set; }

        public SymmetricSecurityKey Value { get; set; }
    }
}
using Microsoft.IdentityModel.Tokens;

namespace PaladinRogue.Libray.Vault.Domain.SharedDataKeys.Create
{
    public class CreateSharedDataKeyCommandDdto
    {
        public string Name { get; set; }

        public SymmetricSecurityKey Value { get; set; }
    }
}
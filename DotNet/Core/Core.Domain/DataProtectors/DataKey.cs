using Microsoft.IdentityModel.Tokens;

namespace PaladinRogue.Libray.Core.Domain.DataProtectors
{
    public class DataKey
    {
        public string Name { get; set; }

        public SymmetricSecurityKey Value { get; set; }
    }
}
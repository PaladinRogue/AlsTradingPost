using Microsoft.IdentityModel.Tokens;

namespace Common.Domain.DataProtectors
{
    public class DataKey
    {
        public string Name { get; set; }

        public SymmetricSecurityKey Value { get; set; }
    }
}
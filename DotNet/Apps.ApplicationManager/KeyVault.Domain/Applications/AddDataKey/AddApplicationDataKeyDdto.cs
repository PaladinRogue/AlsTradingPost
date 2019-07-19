using Microsoft.IdentityModel.Tokens;

namespace KeyVault.Domain.Applications.AddDataKey
{
    internal class AddApplicationDataKeyDdto
    {
        public int Type { get; set; }

        public SymmetricSecurityKey Value { get; set; }
    }
}
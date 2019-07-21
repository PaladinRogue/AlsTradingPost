using Microsoft.IdentityModel.Tokens;

namespace KeyVault.Domain.Applications.AddDataKey
{
    internal class AddApplicationDataKeyDdto
    {
        public string Type { get; set; }

        public SymmetricSecurityKey Value { get; set; }
    }
}
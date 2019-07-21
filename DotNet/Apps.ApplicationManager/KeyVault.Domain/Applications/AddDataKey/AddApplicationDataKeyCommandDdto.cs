using Microsoft.IdentityModel.Tokens;

namespace KeyVault.Domain.Applications.AddDataKey
{
    public class AddApplicationDataKeyCommandDdto
    {
        public string Type { get; set; }

        public SymmetricSecurityKey Value { get; set; }
    }
}
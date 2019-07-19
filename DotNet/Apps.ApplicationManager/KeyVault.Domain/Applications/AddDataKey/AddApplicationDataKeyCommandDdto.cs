using Microsoft.IdentityModel.Tokens;

namespace KeyVault.Domain.Applications.AddDataKey
{
    public class AddApplicationDataKeyCommandDdto
    {
        public int Type { get; set; }

        public SymmetricSecurityKey Value { get; set; }
    }
}
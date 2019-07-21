using Microsoft.IdentityModel.Tokens;

namespace KeyVault.Broker.Domain
{
    public class DataKey<T>
    {
        public T Type { get; set; }

        public SymmetricSecurityKey Value { get; set; }
    }
}
using System;
using Microsoft.IdentityModel.Tokens;

namespace KeyVault.Broker.Domain
{
    public class DataKey<T> where T : struct, Enum
    {
        public T Type { get; set; }

        public SymmetricSecurityKey Value { get; set; }
    }
}
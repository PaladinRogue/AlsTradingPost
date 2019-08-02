using System;

namespace Vault.Broker.Setup.DataKeys
{
    public class DataKeyNotFoundException : Exception
    {
        public DataKeyNotFoundException(string keyName) : base(GenerateMessage(keyName))
        {
        }

        public DataKeyNotFoundException(string keyName, Exception innerException) : base(GenerateMessage(keyName), innerException)
        {
        }

        private static string GenerateMessage(string keyName)
        {
            return $"Data key not found for name: {keyName}";
        }
    }
}
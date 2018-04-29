using Microsoft.IdentityModel.Tokens;

namespace Common.Application.Encryption.Interfaces
{
    public interface IEncryptionFactory
    {
	    string Enrypt<T>(T data, SymmetricSecurityKey securityKey);
	    T Decrypt<T>(string encryptedData, SymmetricSecurityKey securityKey);
    }
}

using Microsoft.IdentityModel.Tokens;

namespace Common.Resources.Encryption
{
    public interface IEncryptionFactory
    {
	    string Encrypt<T>(T data, SymmetricSecurityKey securityKey);

	    T Decrypt<T>(string encryptedData, SymmetricSecurityKey securityKey);

	    SymmetricSecurityKey CreateKey();
    }
}

using Microsoft.IdentityModel.Tokens;

namespace PaladinRogue.Libray.Core.Common.Encryption
{
    public interface IEncryptionFactory
    {
	    string Encrypt<T>(T data, SymmetricSecurityKey securityKey);

	    T Decrypt<T>(string encryptedData, SymmetricSecurityKey securityKey);

	    SymmetricSecurityKey CreateKey();
    }
}

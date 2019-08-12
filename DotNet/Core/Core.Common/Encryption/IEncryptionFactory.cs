using Microsoft.IdentityModel.Tokens;

namespace PaladinRogue.Library.Core.Common.Encryption
{
    public interface IEncryptionFactory
    {
	    string Encrypt<T>(T data, SymmetricSecurityKey securityKey);

	    T Decrypt<T>(string encryptedData, SymmetricSecurityKey securityKey);

	    SymmetricSecurityKey CreateKey();
    }
}

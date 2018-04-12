using System;
using System.IO;
using System.Security.Cryptography;
using Common.Api.Factories.Interfaces;
using Microsoft.IdentityModel.Tokens;
using Newtonsoft.Json;

namespace Common.Api.Factories
{
    public class EncryptionFactory : IEncryptionFactory
    {
	    public string Enrypt<T>(T data, SymmetricSecurityKey securityKey)
	    {
		    var textToEncode = JsonConvert.SerializeObject(data);
		    byte[] encrypted;

		    var keyBytes = securityKey.Key;
		    
		    using (Aes aesAlg = Aes.Create())
		    {
			    if (aesAlg == null)
			    {
				    throw new NullReferenceException(nameof(aesAlg));
			    }

			    aesAlg.Key = keyBytes;
			    var iv = aesAlg.IV;

			    ICryptoTransform encryptor = aesAlg.CreateEncryptor(aesAlg.Key, aesAlg.IV);

			    using (MemoryStream msEncrypt = new MemoryStream())
			    {
				    using (CryptoStream csEncrypt = new CryptoStream(msEncrypt, encryptor, CryptoStreamMode.Write))
				    {
					    using (StreamWriter swEncrypt = new StreamWriter(csEncrypt))
					    {
						    // write iv
						    msEncrypt.Write(iv, 0, iv.Length);
						    //Write all data to the stream.
						    swEncrypt.Write(textToEncode);
					    }

					    encrypted = msEncrypt.ToArray();
				    }
			    }
		    }

		    return Convert.ToBase64String(encrypted);
		}

	    public T Decrypt<T>(string cipherText, SymmetricSecurityKey securityKey)
	    {
		    var fullCipher = Convert.FromBase64String(cipherText);

		    if (fullCipher == null || fullCipher.Length <= 0)
			    throw new ArgumentNullException(nameof(cipherText));
            
            string plaintext;
		    var keyBytes = securityKey.Key;

		    using (Aes aesAlg = Aes.Create())
			{
				if (aesAlg == null)
				{
					throw new NullReferenceException(nameof(aesAlg));
				}

				aesAlg.Key = keyBytes;

                using (MemoryStream msDecrypt = new MemoryStream(fullCipher))
			    {
				    var iv = new byte[16];
				    msDecrypt.Read(iv, 0, 16);
				    aesAlg.IV = iv;

				    ICryptoTransform decryptor = aesAlg.CreateDecryptor(aesAlg.Key, aesAlg.IV);

				    using (CryptoStream csDecrypt = new CryptoStream(msDecrypt, decryptor, CryptoStreamMode.Read))
				    {
					    using (StreamReader srDecrypt = new StreamReader(csDecrypt))
					    {
						    plaintext = srDecrypt.ReadToEnd();
					    }
				    }
			    }
		    }

		    return JsonConvert.DeserializeObject<T>(plaintext);
		}
    }
}

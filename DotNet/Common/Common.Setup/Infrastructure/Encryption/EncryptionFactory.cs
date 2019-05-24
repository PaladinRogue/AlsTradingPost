using System;
using System.IO;
using System.Security.Cryptography;
using Common.Resources.Encryption;
using Microsoft.IdentityModel.Tokens;
using Newtonsoft.Json;

namespace Common.Setup.Infrastructure.Encryption
{
    public class EncryptionFactory : IEncryptionFactory
    {
	    public string Enrypt<T>(T data, SymmetricSecurityKey securityKey)
	    {
		    string textToEncode = JsonConvert.SerializeObject(data);
		    byte[] encrypted;

		    byte[] keyBytes = securityKey.Key;
		    
		    using (Aes aesAlg = Aes.Create())
		    {
			    if (aesAlg == null)
			    {
				    throw new NullReferenceException(nameof(aesAlg));
			    }

			    aesAlg.Key = keyBytes;
			    byte[] iv = aesAlg.IV;

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
		    byte[] fullCipher = Convert.FromBase64String(cipherText);

		    if (fullCipher == null || fullCipher.Length <= 0)
			    throw new ArgumentNullException(nameof(cipherText));
            
            string plaintext;
		    byte[] keyBytes = securityKey.Key;

		    using (Aes aesAlg = Aes.Create())
			{
				if (aesAlg == null)
				{
					throw new NullReferenceException(nameof(aesAlg));
				}

				aesAlg.Key = keyBytes;

                using (MemoryStream msDecrypt = new MemoryStream(fullCipher))
			    {
				    byte[] iv = new byte[16];
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

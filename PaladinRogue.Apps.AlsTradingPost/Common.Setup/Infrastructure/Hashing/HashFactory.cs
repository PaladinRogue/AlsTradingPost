using System;
using System.Security.Cryptography;
using System.Text;

namespace Common.Setup.Infrastructure.Hashing
{
    public class HashFactory : IHashFactory
    {
        public Hashing GenerateHash<T>(T data)
        {
            string salt = GetSalt();
            return new Hashing
            {
                Salt = salt,
                Hash = GetHash(data + salt)
            };
        }
        
        private static string GetSalt() {
            byte[] bytes = new byte[128 / 8];
            using (RandomNumberGenerator keyGenerator = RandomNumberGenerator.Create()) {
                keyGenerator.GetBytes(bytes);
 
                return BitConverter.ToString(bytes).Replace("-", "").ToLower();
            }
        }
        
        private static string GetHash(string text) {
            using (SHA256 sha256 = SHA256.Create()) {
                byte[] hashedBytes = sha256.ComputeHash(Encoding.UTF8.GetBytes(text));
   
                return BitConverter.ToString(hashedBytes).Replace("-", "").ToLower();
            }
        }
    }
}
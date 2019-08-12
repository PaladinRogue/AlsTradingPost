using System;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;
using PaladinRogue.Library.Core.Domain.DataProtectors;

namespace PaladinRogue.Library.Core.Setup.Infrastructure.Hashing
{
    public class Sha256HashFactory : IHashFactory
    {
        public Task<HashSet> GenerateHashAsync<T>(T data, string salt = null)
        {
            salt = salt ?? GetSalt();
            return Task.FromResult(new HashSet
            {
                Salt = salt,
                Hash = GetHash(data + salt)
            });
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
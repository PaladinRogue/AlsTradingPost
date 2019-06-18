using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using Common.Resources;

namespace Common.Domain.Models.PasswordProtection
{
    public class ProtectedPassword : ValueObject
    {
        protected ProtectedPassword()
        {
        }

        public static ProtectedPassword Create(string hash, string salt)
        {
            return new ProtectedPassword
            {
                Hash = hash,
                Salt = salt
            };
        }

        [MaxLength(FieldSizes.Protected)]
        [Required]
        public string Hash { get; protected set; }

        [MaxLength(FieldSizes.Extended)]
        [Required]
        public string Salt { get; protected set; }

        protected override IEnumerable<object> GetEqualityComponents()
        {
            yield return Hash;
            yield return Salt;
        }
    }
}
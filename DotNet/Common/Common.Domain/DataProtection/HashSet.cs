using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using Common.Resources;
using Common.Resources.ValueObjects;

namespace Common.Domain.DataProtection
{
    public class HashSet : ValueObject<HashSet>
    {
        [Required]
        [MaxLength(FieldSizes.Extended)]
        public string Salt { get; set; }

        [Required]
        [MaxLength(FieldSizes.Protected)]
        public string Hash { get; set; }

        protected override IEnumerable<object> GetEqualityComponents()
        {
            yield return Salt;
            yield return Hash;
        }
    }
}
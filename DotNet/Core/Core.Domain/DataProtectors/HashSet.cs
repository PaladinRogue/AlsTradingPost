using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using PaladinRogue.Libray.Core.Common;
using PaladinRogue.Libray.Core.Common.ValueObjects;

namespace PaladinRogue.Libray.Core.Domain.DataProtectors
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
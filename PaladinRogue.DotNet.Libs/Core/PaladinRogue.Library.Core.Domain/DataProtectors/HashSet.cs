using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using PaladinRogue.Library.Core.Common;
using PaladinRogue.Library.Core.Common.ValueObjects;

namespace PaladinRogue.Library.Core.Domain.DataProtectors
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
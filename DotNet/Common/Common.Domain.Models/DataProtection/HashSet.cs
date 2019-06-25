using System.Collections.Generic;

namespace Common.Domain.Models.DataProtection
{
    public class HashSet : ValueObject
    {
        public string Salt { get; set; }

        public string Hash { get; set; }

        protected override IEnumerable<object> GetEqualityComponents()
        {
            yield return Salt;
            yield return Hash;
        }
    }
}
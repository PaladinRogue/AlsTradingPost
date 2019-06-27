using System.Collections.Generic;
using Common.Domain.ValueObjects;

namespace Common.Domain.DataProtection
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
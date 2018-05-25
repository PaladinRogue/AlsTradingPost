using System.Collections.Generic;
using System.Linq;
using AlsTradingPost.Resources;

namespace AlsTradingPost.Setup.Infrastructure.Authorization
{
    public static class PersonaPolicyMapper
    {
        private static readonly IReadOnlyDictionary<PersonaFlags, string> PersonaPolicyDictionary = new Dictionary<PersonaFlags, string>
        {
           [PersonaFlags.Admin]  = PersonaPolicies.Admin,
           [PersonaFlags.Trader] = PersonaPolicies.Trader
        };

        public static IReadOnlyDictionary<PersonaFlags, string> GetMap()
        {
            return PersonaPolicyDictionary;
        }

        public static PersonaFlags FromPolicy(string policy)
        {
            return PersonaPolicyDictionary.FirstOrDefault(x => x.Value == policy).Key;
        }

        public static string FromPersona(PersonaFlags persona)
        {
            return PersonaPolicyDictionary.ContainsKey(persona) ? PersonaPolicyDictionary[persona] : null;
        }
    }
}
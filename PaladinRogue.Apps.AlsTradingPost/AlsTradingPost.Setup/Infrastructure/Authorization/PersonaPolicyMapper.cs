using System.Collections.Generic;
using System.Linq;
using AlsTradingPost.Resources;

namespace AlsTradingPost.Setup.Infrastructure.Authorization
{
    public static class PersonaPolicyMapper
    {
        private static readonly IDictionary<PersonaFlags, string> PersonaPolicyDictionary = new Dictionary<PersonaFlags, string>();
        
        static PersonaPolicyMapper()
        {
            PersonaPolicyDictionary.Add(PersonaFlags.Admin, PersonaPolicies.Admin);
            PersonaPolicyDictionary.Add(PersonaFlags.Player, PersonaPolicies.Player);
        }

        public static IDictionary<PersonaFlags, string> GetMap()
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
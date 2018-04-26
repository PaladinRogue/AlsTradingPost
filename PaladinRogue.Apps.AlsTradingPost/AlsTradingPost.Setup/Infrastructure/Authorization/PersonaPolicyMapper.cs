using System.Collections.Generic;
using System.Linq;
using AlsTradingPost.Resources;

namespace AlsTradingPost.Setup.Infrastructure.Authorization
{
    public static class PersonaPolicyMapper
    {
        private static readonly IDictionary<Persona, string> PersonaPolicyDictionary = new Dictionary<Persona, string>();
        
        static PersonaPolicyMapper()
        {
            PersonaPolicyDictionary.Add(Persona.Admin, PersonaPolicies.Admin);
            PersonaPolicyDictionary.Add(Persona.Player, PersonaPolicies.Player);
        }

        public static IDictionary<Persona, string> GetMap()
        {
            return PersonaPolicyDictionary;
        }

        public static Persona FromPolicy(string policy)
        {
            return PersonaPolicyDictionary.FirstOrDefault(x => x.Value == policy).Key;
        }

        public static string FromPersona(Persona persona)
        {
            return PersonaPolicyDictionary.ContainsKey(persona) ? PersonaPolicyDictionary[persona] : null;
        }
    }
}
using System.Collections.Generic;
using System.Linq;

namespace AlsTradingPost.Resources
{
    public static class PersonTypeMapper
    {
        private static readonly IDictionary<PersonaType, PersonaFlags> PersonaTypeMap = new Dictionary<PersonaType, PersonaFlags>
        {
            { PersonaType.Admin, PersonaFlags.Admin },
            { PersonaType.Trader, PersonaFlags.Trader }
        };

        public static PersonaFlags GetPersonaFlags(params PersonaType[] types)
        {
            return types.Aggregate(PersonaFlags.None,
                (current, personaType) => current | PersonaTypeMap[personaType]
            );
        }
    }
}
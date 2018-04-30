using System;
using System.Collections.Generic;
using System.Linq;
using Common.Api.Builders;

namespace AlsTradingPost.Resources
{
    public static class PersonTypeMapper
    {
        private static readonly IDictionary<PersonaType, PersonaFlags> PersonaTypeMap = new Dictionary<PersonaType, PersonaFlags>
        {
            { PersonaType.Admin, PersonaFlags.Admin },
            { PersonaType.Player, PersonaFlags.Player }
        };

        public static PersonaFlags GetPersonaFlags(params PersonaType[] types)
        {
            return types.Aggregate(PersonaFlags.None,
                (current, personaType) => current | PersonaTypeMap[personaType]
            );
        }
    }
}
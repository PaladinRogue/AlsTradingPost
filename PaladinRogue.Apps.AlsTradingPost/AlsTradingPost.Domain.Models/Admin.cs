using AlsTradingPost.Domain.Models.Interfaces;
using AlsTradingPost.Resources;
using Common.Domain.Models;

namespace AlsTradingPost.Domain.Models
{
    public class Admin : VersionedEntity, IPersona
    {
        public PersonaType TypeDiscriminator => PersonaType.Admin;
    }
}

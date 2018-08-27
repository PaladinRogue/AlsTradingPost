using AlsTradingPost.Resources;
using Common.Domain.Models.Interfaces;

namespace AlsTradingPost.Domain.Models.Interfaces
{
    public interface IPersona : IEntity
    {
        PersonaType TypeDiscriminator { get; }
    }
}

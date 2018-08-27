using AlsTradingPost.Domain.Models.Interfaces;
using AlsTradingPost.Domain.PersonaDomain.Models;
using AutoMapper;

namespace AlsTradingPost.Domain.PersonaDomain.Mappings
{
    public class PersonanDomainMappingProfile : Profile
    {
        public PersonanDomainMappingProfile()
        {
            CreateMap<IPersona, PersonaProjection>()
                .ForMember(d => d.PersonaType, opts => opts.MapFrom(s => s.TypeDiscriminator));
        }
    }
}

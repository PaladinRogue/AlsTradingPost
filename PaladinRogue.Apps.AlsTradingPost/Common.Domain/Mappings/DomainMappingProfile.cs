using AutoMapper;
using Common.Domain.Interfaces;
using Common.Domain.Models.Interfaces;
using Common.Domain.Resolvers;

namespace Common.Domain.Mappings
{
    public class DomainMappingProfile : Profile
    {
        public DomainMappingProfile()
        {
            CreateMap<IEntity, IVersionedProjection>()
                .ForMember(dest => dest.Version, opts => opts.ResolveUsing<OutboundConcurrencyTokenResolver>());

            CreateMap<IVersionedDdto, IEntity>()
                .ForMember(dest => dest.Version, opts => opts.ResolveUsing<InboundConcurrencyTokenResolver>());
        }
    }
}

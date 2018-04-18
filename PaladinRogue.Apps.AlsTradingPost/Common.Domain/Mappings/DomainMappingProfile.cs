using AutoMapper;
using Common.Domain.Concurrency;
using Common.Domain.Concurrency.Interfaces;
using Common.Domain.Models.Interfaces;

namespace Common.Domain.Mappings
{
    public class DomainMappingProfile : Profile
    {
        public DomainMappingProfile()
        {
            CreateMap<IVersionedEntity, IVersionedProjection>()
                .ForMember(dest => dest.Version, opts => opts.ResolveUsing<OutboundConcurrencyTokenResolver>());

            CreateMap<IVersionedDdto, IVersionedEntity>()
                .ForMember(dest => dest.Version, opts => opts.ResolveUsing<InboundConcurrencyTokenResolver>());
        }
    }
}

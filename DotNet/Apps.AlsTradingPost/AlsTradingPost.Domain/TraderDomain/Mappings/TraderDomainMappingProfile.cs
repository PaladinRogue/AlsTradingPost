using AlsTradingPost.Domain.Models;
using AlsTradingPost.Domain.TraderDomain.Models;
using AutoMapper;
using Common.Domain.Concurrency.Interfaces;
using Common.Domain.Models.Interfaces;

namespace AlsTradingPost.Domain.TraderDomain.Mappings
{
    public class TraderDomainMappingProfile : Profile
    {
        public TraderDomainMappingProfile()
        {
            CreateMap<Trader, TraderProjection>()
                .IncludeBase<IVersionedEntity, IVersionedProjection>();
            CreateMap<RegisterTraderDdto, Trader>()
                .ForMember(d => d.Id, opts => opts.MapFrom(s => s.UserId));
            CreateMap<Trader, RegisteredTraderProjection>()
                .IncludeBase<IVersionedEntity, IVersionedProjection>();
            CreateMap<UpdateTraderDdto, Trader>()
                .IncludeBase<IVersionedDdto, IVersionedEntity>();
        }
    }
}
using AlsTradingPost.Domain.Models;
using AlsTradingPost.Domain.TraderDomain.Models;
using AutoMapper;

namespace AlsTradingPost.Domain.TraderDomain.Mappings
{
    public class TraderDomainMappingProfile : Profile
    {
        
        public TraderDomainMappingProfile()
        {
            CreateMap<CreateTraderDdto, Trader>();
            CreateMap<Trader, TraderProjection>();
        }
    }
}
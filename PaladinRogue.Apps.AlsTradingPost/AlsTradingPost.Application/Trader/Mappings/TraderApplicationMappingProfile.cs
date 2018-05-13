using AlsTradingPost.Application.Trader.Models;
using AlsTradingPost.Domain.TraderDomain.Models;
using AutoMapper;

namespace AlsTradingPost.Application.Trader.Mappings
{
    public class TraderApplicationMappingProfile : Profile
    {
        public TraderApplicationMappingProfile()
        {
            CreateMap<TraderProjection, RegisteredTraderAdto>();
            CreateMap<RegisterTraderAdto, CreateTraderDdto>();
            CreateMap<TraderProjection, TraderAdto>();
            CreateMap<UpdateTraderAdto, UpdateTraderDdto>();
        }
    }
}

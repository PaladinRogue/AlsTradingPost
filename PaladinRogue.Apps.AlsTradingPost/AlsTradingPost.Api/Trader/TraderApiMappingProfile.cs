using AlsTradingPost.Application.Trader.Models;
using AutoMapper;

namespace AlsTradingPost.Api.Trader
{
    public class TraderApiMappingProfile : Profile
    {
        public TraderApiMappingProfile()
        {
            CreateMap<RegisteredTraderAdto, TraderResource>();
            CreateMap<TraderTemplate, RegisterTraderAdto>();
        }
    }
}

using AlsTradingPost.Application.Trader.Models;

namespace AlsTradingPost.Api.Trader
{
    public class TraderApiMappingProfile : AutoMapper.Profile
    {
        public TraderApiMappingProfile()
        {
            CreateMap<RegisteredTraderAdto, TraderResource>();
            CreateMap<TraderTemplate, RegisterTraderAdto>();
        }
    }
}

using AlsTradingPost.Domain.Models;
using AlsTradingPost.Domain.PlayerDomain.Models;
using AutoMapper;

namespace AlsTradingPost.Domain.PlayerDomain.Mappings
{
    public class PlayerDomainMappingProfile : Profile
    {
        
        public PlayerDomainMappingProfile()
        {
            CreateMap<CreatePlayerDdto, Player>();
            CreateMap<Player, PlayerProjection>();
        }
    }
}
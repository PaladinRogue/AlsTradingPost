using AlsTradingPost.Domain.PlayerDomain.Models;
using Common.Domain.Services.Interfaces;

namespace AlsTradingPost.Domain.PlayerDomain.Interfaces
{
    public interface IPlayerCommandService : ICreateCommandService<CreatePlayerDdto, PlayerProjection>
    {
    }
}

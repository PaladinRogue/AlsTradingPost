using AlsTradingPost.Domain.TraderDomain.Models;
using Common.Domain.Services.Interfaces;

namespace AlsTradingPost.Domain.TraderDomain.Interfaces
{
    public interface ITraderCommandService : ICreateCommandService<CreateTraderDdto, TraderProjection>
    {
    }
}

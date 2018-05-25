using AlsTradingPost.Domain.Models;
using Common.Domain.Services.Command;

namespace AlsTradingPost.Domain.TraderDomain.Interfaces
{
    public interface ITraderCommandService : ICreateCommandService<Trader>, IUpdateCommandService<Trader>
    {
    }
}

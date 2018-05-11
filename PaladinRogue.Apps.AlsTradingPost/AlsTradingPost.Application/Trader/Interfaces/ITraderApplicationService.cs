using AlsTradingPost.Application.Trader.Models;

namespace AlsTradingPost.Application.Trader.Interfaces
{
    public interface ITraderApplicationService
    {
        RegisteredTraderAdto Register(RegisterTraderAdto traderAdto);
    }
}

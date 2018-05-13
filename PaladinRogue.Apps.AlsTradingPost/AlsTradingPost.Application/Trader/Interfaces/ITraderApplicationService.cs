using System;
using AlsTradingPost.Application.Trader.Models;

namespace AlsTradingPost.Application.Trader.Interfaces
{
    public interface ITraderApplicationService
    {
        RegisteredTraderAdto Register(RegisterTraderAdto traderAdto);
        
        TraderAdto GetById(Guid id);
    }
}

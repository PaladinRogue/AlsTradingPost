using System;
using AlsTradingPost.Application.Trader.Models;

namespace AlsTradingPost.Application.Trader.Interfaces
{
    public interface ITraderApplicationService
    {
        RegisteredTraderAdto Register(RegisterTraderAdto registerTraderAdto);
        
        TraderAdto GetById(Guid id);
        
        TraderAdto Update(UpdateTraderAdto updateTraderAdto);
    }
}

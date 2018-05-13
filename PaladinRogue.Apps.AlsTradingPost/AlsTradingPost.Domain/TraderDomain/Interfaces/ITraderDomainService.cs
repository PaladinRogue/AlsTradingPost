using AlsTradingPost.Domain.TraderDomain.Models;

namespace AlsTradingPost.Domain.TraderDomain.Interfaces
{
    public interface ITraderDomainService
    {
        RegisteredTraderProjection Register(RegisterTraderDdto registerTraderDdto);
    }
}
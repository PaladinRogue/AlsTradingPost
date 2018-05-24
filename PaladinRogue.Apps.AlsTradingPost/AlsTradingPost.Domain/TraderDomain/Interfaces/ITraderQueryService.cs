using AlsTradingPost.Domain.Models;
using Common.Domain.Services.Domain;
using Common.Domain.Services.Query;

namespace AlsTradingPost.Domain.TraderDomain.Interfaces
{
    public interface ITraderQueryService : IGetByIdService<Trader>, ICheckConcurrencyService
    {
    }
}
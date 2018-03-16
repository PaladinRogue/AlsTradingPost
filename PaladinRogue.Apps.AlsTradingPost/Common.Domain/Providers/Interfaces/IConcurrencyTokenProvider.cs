using Common.Domain.Interfaces;
using Common.Domain.Models.Interfaces;

namespace Common.Domain.Providers.Interfaces
{
    public interface IConcurrencyTokenProvider
    {
        int GetConcurrencyToken(IEntity entity);
        byte[] GetConcurrencyToken(IVersionedDdto entity);
    }
}

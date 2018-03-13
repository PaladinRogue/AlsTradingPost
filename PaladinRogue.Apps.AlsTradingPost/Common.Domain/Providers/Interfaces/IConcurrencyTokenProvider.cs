using Common.Domain.Models.Interfaces;

namespace Common.Domain.Providers.Interfaces
{
    public interface IConcurrencyTokenProvider
    {
        int GetVersion(IEntity entity);
    }
}

using Common.Domain.Models.Interfaces;
using Common.Domain.Providers.Interfaces;

namespace Common.Domain.Providers
{
    public class ConcurrencyTokenProvider : IConcurrencyTokenProvider
    {
        public int GetVersion(IEntity entity)
        {
            return entity.GetHashCode();
        }
    }

}

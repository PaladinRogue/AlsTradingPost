using System.Threading.Tasks;

namespace Common.ApplicationServices.Caching
{
    public interface ICacheDecorator<in TKey, in TValue>
    {
        Task AddAsync(TKey key, TValue value);

        Task UpdateAsync(TKey key, TValue value);

        Task RemoveAsync(TKey key);
    }
}
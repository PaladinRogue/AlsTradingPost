using System.Threading.Tasks;
using Common.Messaging.Infrastructure.Interfaces;
using Common.Messaging.Infrastructure.Messages;

namespace Common.Messaging.Infrastructure.Factories
{
    public interface IMessageFactory
    {
        Task<IPreparedMessage> CreateAsync(IMessage message);
    }
}

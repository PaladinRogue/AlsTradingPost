using System.Threading.Tasks;
using Messaging.Common;
using Messaging.Setup.Infrastructure.Interfaces;

namespace Messaging.Setup.Infrastructure.Factories
{
    public interface IMessageFactory
    {
        Task<IPreparedMessage> CreateAsync(IMessage message);
    }
}

using System.Threading.Tasks;
using PaladinRogue.Libray.Messaging.Common.Messages;
using PaladinRogue.Libray.Messaging.Setup.Infrastructure.Interfaces;

namespace PaladinRogue.Libray.Messaging.Setup.Infrastructure.Factories
{
    public interface IMessageFactory
    {
        Task<IPreparedMessage> CreateAsync(IMessage message);
    }
}

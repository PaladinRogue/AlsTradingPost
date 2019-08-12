using System.Threading.Tasks;
using PaladinRogue.Library.Messaging.Common.Messages;
using PaladinRogue.Library.Messaging.Setup.Infrastructure.Interfaces;

namespace PaladinRogue.Library.Messaging.Setup.Infrastructure.Factories
{
    public interface IMessageFactory
    {
        Task<IPreparedMessage> CreateAsync(IMessage message);
    }
}

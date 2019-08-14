using System.Threading.Tasks;
using PaladinRogue.Library.Messaging.Common.Messages;

namespace PaladinRogue.Library.Messaging.Common.Senders
{
    public interface IMessageSender
    {
        Task SendAsync(IMessage message);
    }
}
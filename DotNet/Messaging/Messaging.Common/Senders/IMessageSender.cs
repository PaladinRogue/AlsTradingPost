using System.Threading.Tasks;
using PaladinRogue.Libray.Messaging.Common.Messages;

namespace PaladinRogue.Libray.Messaging.Common.Senders
{
    public interface IMessageSender
    {
        Task SendAsync(IMessage message);
    }
}
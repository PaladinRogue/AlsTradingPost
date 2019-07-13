using System.Threading.Tasks;
using Common.Messaging.Infrastructure.Messages;

namespace Common.Messaging.Infrastructure.Senders
{
    public interface IMessageSender
    {
        Task SendAsync(IMessage message);
    }
}
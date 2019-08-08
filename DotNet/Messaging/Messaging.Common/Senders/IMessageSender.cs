using System.Threading.Tasks;
using Messaging.Common;

namespace Messaging.Setup.Infrastructure.Senders
{
    public interface IMessageSender
    {
        Task SendAsync(IMessage message);
    }
}
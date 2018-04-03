using System.Threading.Tasks;

namespace Common.Messaging.Interfaces
{
    public interface IMessageSender
    {
	    Task SendAsync(IMessage message);
    }
}

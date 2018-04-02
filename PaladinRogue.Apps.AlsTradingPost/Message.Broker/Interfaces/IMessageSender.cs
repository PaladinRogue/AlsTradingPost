using System.Threading.Tasks;

namespace Message.Broker.Interfaces
{
    public interface IMessageSender
    {
	    Task SendAsync(IMessage message);
    }
}

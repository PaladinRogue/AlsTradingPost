using System.Threading.Tasks;

namespace Message.Broker.Messages.Interfaces
{
	public interface IMessageDispatcher
	{
		Task DispatchMessagesAsync();
	}
}
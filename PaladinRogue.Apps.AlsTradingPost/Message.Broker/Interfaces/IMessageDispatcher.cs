using System.Threading.Tasks;

namespace Message.Broker.Interfaces
{
	public interface IMessageDispatcher
	{
		Task DispatchMessagesAsync();
	}
}
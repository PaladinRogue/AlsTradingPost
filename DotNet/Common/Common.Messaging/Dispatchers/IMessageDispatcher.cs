using System.Threading.Tasks;

namespace Common.Messaging.Dispatchers
{
	public interface IMessageDispatcher
	{
		Task DispatchMessagesAsync();
	}
}
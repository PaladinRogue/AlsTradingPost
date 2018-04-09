using System.Threading.Tasks;

namespace Common.Messaging.Message.Interfaces
{
	public interface IMessageDispatcher
	{
		Task DispatchMessagesAsync();
	}
}
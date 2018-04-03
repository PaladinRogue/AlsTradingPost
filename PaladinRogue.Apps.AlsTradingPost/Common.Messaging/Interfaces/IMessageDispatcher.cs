using System.Threading.Tasks;

namespace Common.Messaging.Interfaces
{
	public interface IMessageDispatcher
	{
		Task DispatchMessagesAsync();
	}
}
using System.Threading.Tasks;

namespace Common.Messaging.Infrastructure.Dispatchers
{
	public interface IMessageDispatcher
	{
		Task DispatchMessagesAsync();
	}
}
using System.Threading.Tasks;

namespace Messaging.Setup.Infrastructure.Dispatchers
{
	public interface IMessageDispatcher
	{
		Task DispatchMessagesAsync();
	}
}
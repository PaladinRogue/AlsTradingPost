using System.Threading.Tasks;

namespace PaladinRogue.Libray.Messaging.Common.Dispatchers
{
	public interface IMessageDispatcher
	{
		Task DispatchMessagesAsync();
	}
}
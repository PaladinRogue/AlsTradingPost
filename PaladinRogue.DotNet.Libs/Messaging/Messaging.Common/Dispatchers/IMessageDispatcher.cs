using System.Threading.Tasks;

namespace PaladinRogue.Library.Messaging.Common.Dispatchers
{
	public interface IMessageDispatcher
	{
		Task DispatchMessagesAsync();
	}
}
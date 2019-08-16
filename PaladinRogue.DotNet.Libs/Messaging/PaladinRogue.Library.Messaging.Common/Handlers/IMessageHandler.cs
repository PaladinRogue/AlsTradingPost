using System.Threading.Tasks;

namespace PaladinRogue.Library.Messaging.Common.Handlers
{
	public interface IMessageHandler
	{
		Task RegisterAsync();
	}
}

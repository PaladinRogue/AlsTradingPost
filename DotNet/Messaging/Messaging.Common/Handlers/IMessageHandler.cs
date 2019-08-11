using System.Threading.Tasks;

namespace PaladinRogue.Libray.Messaging.Common.Handlers
{
	public interface IMessageHandler
	{
		Task RegisterAsync();
	}
}

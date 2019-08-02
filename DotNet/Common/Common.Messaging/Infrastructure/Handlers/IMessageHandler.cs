using System.Threading.Tasks;

namespace Common.Messaging.Infrastructure.Handlers
{
	public interface IMessageHandler
	{
		Task RegisterAsync();
	}
}

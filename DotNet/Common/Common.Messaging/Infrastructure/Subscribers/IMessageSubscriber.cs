using System.Threading.Tasks;

namespace Common.Messaging.Infrastructure.Subscribers
{
	public interface IMessageSubscriber
	{
		Task SubscribeAsync();
	}
}

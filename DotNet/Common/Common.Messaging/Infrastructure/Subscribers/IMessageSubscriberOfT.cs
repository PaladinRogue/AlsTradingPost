using System.Threading.Tasks;
using Common.Messaging.Infrastructure.Messages;

namespace Common.Messaging.Infrastructure.Subscribers
{
	public interface IMessageSubscriber<in T> where T : IMessage
	{
		Task HandleAsync(T message);
	}
}

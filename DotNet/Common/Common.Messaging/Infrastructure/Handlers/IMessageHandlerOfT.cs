using System.Threading.Tasks;
using Common.Messaging.Infrastructure.Messages;

namespace Common.Messaging.Infrastructure.Handlers
{
	public interface IMessageHandler<in T> where T : IMessage
	{
		Task ExecuteAsync(T message);
	}
}

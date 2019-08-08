using System.Threading.Tasks;
using Messaging.Common;

namespace Messaging.Setup.Infrastructure.Handlers
{
	public interface IMessageHandler<in T> where T : IMessage
	{
		Task ExecuteAsync(T message);
	}
}

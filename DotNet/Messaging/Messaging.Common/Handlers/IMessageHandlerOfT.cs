using System.Threading.Tasks;
using PaladinRogue.Library.Messaging.Common.Messages;

namespace PaladinRogue.Library.Messaging.Common.Handlers
{
	public interface IMessageHandler<in T> where T : IMessage
	{
		Task ExecuteAsync(T message);
	}
}

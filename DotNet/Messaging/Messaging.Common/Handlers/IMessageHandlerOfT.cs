using System.Threading.Tasks;
using PaladinRogue.Libray.Messaging.Common.Messages;

namespace PaladinRogue.Libray.Messaging.Common.Handlers
{
	public interface IMessageHandler<in T> where T : IMessage
	{
		Task ExecuteAsync(T message);
	}
}

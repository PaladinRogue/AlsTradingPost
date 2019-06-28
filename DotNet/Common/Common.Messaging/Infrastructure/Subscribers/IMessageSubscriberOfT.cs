using Common.Messaging.Infrastructure.Interfaces;
using Common.Messaging.Infrastructure.Messages;

namespace Common.Messaging.Infrastructure.Subscribers
{
	public interface IMessageSubscriber<in T> where T : IMessage
	{
		void Handle(T message);
	}
}

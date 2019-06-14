using Common.Messaging.Infrastructure.Interfaces;

namespace Common.Messaging.Infrastructure.Subscribers
{
	public interface IMessageSubscriber<in T> where T : IMessage
	{
		void Handle(T message);
	}
}

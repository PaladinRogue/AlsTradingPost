using Common.Messaging.Message.Interfaces;

namespace Common.Messaging.Subscribers
{
	public interface IMessageSubscriber<in T> where T : IMessage
	{
		void Handle(T message);
	}
}

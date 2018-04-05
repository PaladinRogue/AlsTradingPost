namespace Common.Messaging.Message.Interfaces
{
	public interface IMessageSubscriber<in T> where T : IMessage
	{
		void Handle(T message);
	}
}

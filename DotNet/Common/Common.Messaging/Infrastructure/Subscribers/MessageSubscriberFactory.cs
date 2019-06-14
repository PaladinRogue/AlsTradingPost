using System.Collections.Generic;

namespace Common.Messaging.Infrastructure.Subscribers
{
    public class MessageSubscriberFactory : IMessageSubscriberFactory
	{
		private readonly IEnumerable<IMessageSubscriber> _subscribers;

		public MessageSubscriberFactory(IEnumerable<IMessageSubscriber> subscribers)
		{
			_subscribers = subscribers;
		}

		public void Initialise()
		{
			foreach (IMessageSubscriber messageSubscriber in _subscribers)
			{
				messageSubscriber.Subscribe();
			}
		}
	}
}

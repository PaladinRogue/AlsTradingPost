using System.Collections.Generic;
using Common.Messaging.Message.Interfaces;

namespace Common.Messaging.Message
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

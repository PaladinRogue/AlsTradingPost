using System.Collections.Generic;
using Common.Messaging.Interfaces;

namespace Common.Messaging
{
    public class MessageSubscribers : IMessageSubscribers
	{
		private readonly IEnumerable<IMessageSubscriber> _subscribers;

		public MessageSubscribers(IEnumerable<IMessageSubscriber> subscribers)
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

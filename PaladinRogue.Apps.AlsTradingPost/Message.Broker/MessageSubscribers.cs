using System.Collections.Generic;
using Message.Broker.Interfaces;

namespace Message.Broker
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

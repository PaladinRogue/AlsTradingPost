using System.Collections.Generic;
using System.Threading.Tasks;

namespace Common.Messaging.Infrastructure.Subscribers
{
    public class MessageSubscriberFactory : IMessageSubscriberFactory
	{
		private readonly IEnumerable<IMessageSubscriber> _subscribers;

		public MessageSubscriberFactory(IEnumerable<IMessageSubscriber> subscribers)
		{
			_subscribers = subscribers;
		}

		public async Task InitialiseAsync()
		{
			foreach (IMessageSubscriber messageSubscriber in _subscribers)
			{
				await messageSubscriber.SubscribeAsync();
			}
		}
	}
}

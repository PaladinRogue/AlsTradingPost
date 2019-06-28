using System.Threading.Tasks;
using Common.Messaging.Infrastructure.Directors;
using Common.Messaging.Infrastructure.Interfaces;
using Common.Messaging.Infrastructure.MessageBus;

namespace Common.Messaging.Infrastructure.Dispatchers
{
	public class MessageDispatcher : IMessageDispatcher
	{
		private readonly IPendingMessageProvider _pendingMessageProvider;

		private readonly IMessageBus _messageBus;

		public MessageDispatcher(
		    IPendingMessageProvider pendingMessageProvider,
		    IMessageBus messageBus)
		{
		    _pendingMessageProvider = pendingMessageProvider;
		    _messageBus = messageBus;
		}

		public async Task DispatchMessagesAsync()
		{
			await Task.Run(() => Parallel.ForEach(_pendingMessageProvider.GetAll(),
				message =>
				{
					_messageBus.Publish(message);
				}));
		}
	}
}

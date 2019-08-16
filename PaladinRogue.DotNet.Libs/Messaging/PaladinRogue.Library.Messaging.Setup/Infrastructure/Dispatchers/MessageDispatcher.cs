using System.Threading.Tasks;
using PaladinRogue.Library.Messaging.Common.Dispatchers;
using PaladinRogue.Library.Messaging.Common.MessageBus;
using PaladinRogue.Library.Messaging.Setup.Infrastructure.Directors;

namespace PaladinRogue.Library.Messaging.Setup.Infrastructure.Dispatchers
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

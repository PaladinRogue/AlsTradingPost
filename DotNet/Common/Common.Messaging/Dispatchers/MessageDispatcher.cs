using System.Threading.Tasks;
using Common.Messaging.Infrastructure.Interfaces;
using Common.Messaging.Message.Interfaces;

namespace Common.Messaging.Dispatchers
{
	public class MessageDispatcher : IMessageDispatcher
	{
		private readonly IPendingMessageProvider _pendingMessageProvider;

		private readonly IMessageSender _messageSender;

		public MessageDispatcher(
		    IPendingMessageProvider pendingMessageProvider,
		    IMessageSender messageSender)
		{
		    _pendingMessageProvider = pendingMessageProvider;
		    _messageSender = messageSender;
		}

		public async Task DispatchMessagesAsync()
		{
			await Task.Run(() => Parallel.ForEach(_pendingMessageProvider.GetAll(),
				message => { _messageSender.Send(message); }));
		}
	}
}

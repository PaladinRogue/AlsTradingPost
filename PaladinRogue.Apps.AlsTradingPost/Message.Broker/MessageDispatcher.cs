using System.Threading.Tasks;
using Common.Messaging.Interfaces;
using Message.Broker.Interfaces;

namespace Message.Broker
{
	public class MessageDispatcher : IMessageDispatcher
	{
		private readonly IMessages _messages;
		private readonly IMessageBus _messageBus;

		public MessageDispatcher(IMessages messages, IMessageBus messageBus)
		{
			_messages = messages;
		    _messageBus = messageBus;
		}

		public async Task DispatchMessagesAsync()
		{
			await Task.Run(() => Parallel.ForEach(_messages.GetAll(),
				message => { _messageBus.Publish(message); }));
		}
	}
}

using System.Threading.Tasks;
using Message.Broker.Interfaces;

namespace Message.Broker
{
	public class MessageDispatcher : IMessageDispatcher
	{
		private readonly IMessages _messages;
		private readonly IMessageSender _messageSender;

		public MessageDispatcher(IMessages messages, IMessageSender messageSender)
		{
			_messages = messages;
			_messageSender = messageSender;
		}

		public async Task DispatchMessagesAsync()
		{
			await Task.Run(() => Parallel.ForEach(_messages.GetAll(),
				message => { _messageSender.SendAsync(message); }));
		}
	}
}

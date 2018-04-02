using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Message.Broker.Interfaces;

namespace Message.Broker
{
	public class MessageHandler : IMessageSender, IMessageReceiver
	{
		private readonly IList<IMessage> _messages;

		public MessageHandler()
		{
			_messages = new List<IMessage>(1);
		}

		public async Task SendAsync(IMessage message)
		{
			await Task.Run(() => _messages.Add(message));

			Receive();
		}

		public void Receive()
		{
			foreach (IMessage message in _messages)
			{
				foreach (Delegate @delegate in MessageSubscriberFactory.GetAllOfType(message.GetType()))
				{
					@delegate.DynamicInvoke(message);
				}
			}
		}
	}
}

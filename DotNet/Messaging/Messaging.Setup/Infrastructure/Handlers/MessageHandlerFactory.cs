using System.Collections.Generic;
using System.Threading.Tasks;
using PaladinRogue.Library.Messaging.Common.Handlers;

namespace PaladinRogue.Library.Messaging.Setup.Infrastructure.Handlers
{
    public class MessageHandlerFactory : IMessageHandlerFactory
	{
		private readonly IEnumerable<IMessageHandler> _handlers;

		public MessageHandlerFactory(IEnumerable<IMessageHandler> handlers)
		{
			_handlers = handlers;
		}

		public async Task InitialiseAsync()
		{
			foreach (IMessageHandler messageHandler in _handlers)
			{
				await messageHandler.RegisterAsync();
			}
		}
	}
}

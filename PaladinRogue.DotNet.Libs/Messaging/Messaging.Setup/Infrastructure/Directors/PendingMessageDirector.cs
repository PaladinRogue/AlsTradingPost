using System.Collections.Generic;
using System.Linq;
using PaladinRogue.Library.Messaging.Common.Messages;

namespace PaladinRogue.Library.Messaging.Setup.Infrastructure.Directors
{
    public class PendingMessageDirector : IPendingMessageContainer, IPendingMessageProvider
    {
	    private readonly IList<IMessage> _messages;

	    public PendingMessageDirector()
	    {
		    _messages = new List<IMessage>();
	    }

	    public void Add(IMessage message)
	    {
		    _messages.Add(message);
		}

	    public IEnumerable<IMessage> GetAll()
	    {
		    IList<IMessage> messages =  _messages.ToList();

		    _messages.Clear();

		    return messages;
	    }
    }
}

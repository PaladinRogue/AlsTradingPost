using System.Collections.Generic;
using System.Linq;
using Common.Messaging.Infrastructure.Interfaces;
using Common.Messaging.Infrastructure.Messages;

namespace Common.Messaging.Infrastructure.Directors
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

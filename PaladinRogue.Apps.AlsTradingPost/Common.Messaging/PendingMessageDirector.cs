using System.Collections.Generic;
using Common.Messaging.Interfaces;

namespace Common.Messaging
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
		    return _messages;
	    }
    }
}

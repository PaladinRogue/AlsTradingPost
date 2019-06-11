using System.Collections.Generic;
using Common.Messaging.Infrastructure.Interfaces;

namespace Common.Messaging.Infrastructure
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

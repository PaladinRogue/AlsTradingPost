using System.Collections.Generic;
using Common.Messaging.Interfaces;

namespace Common.Messaging
{
    public class Messages : IMessages
    {
	    private readonly IList<IMessage> _domainEvents;

	    public Messages()
	    {
		    _domainEvents = new List<IMessage>();
	    }

	    public void Send(IMessage message)
	    {
		    _domainEvents.Add(message);
		}

	    public IEnumerable<IMessage> GetAll()
	    {
		    return _domainEvents;

	    }
    }
}

using System.Collections.Generic;
using Common.Messaging.Interfaces;

namespace Common.Messaging
{
    public class Messages : IMessages
    {
	    private readonly IList<IMessage> _messages;

	    public Messages()
	    {
		    _messages = new List<IMessage>();
	    }

	    public void Send(IMessage message)
	    {
		    _messages.Add(message);
		}

	    public IEnumerable<IMessage> GetAll()
	    {
		    return _messages;
	    }
    }
}

using System.Collections.Generic;

namespace Common.Messaging.Interfaces
{
    public interface IMessages
	{
		void Send(IMessage message);
		IEnumerable<IMessage> GetAll();
	}
}

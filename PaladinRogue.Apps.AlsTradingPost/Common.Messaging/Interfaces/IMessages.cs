using System.Collections.Generic;

namespace Common.Messaging.Interfaces
{
    public interface IMessages
	{
		void Send(IMessage messageSubscriber);
		IEnumerable<IMessage> GetAll();
	}
}

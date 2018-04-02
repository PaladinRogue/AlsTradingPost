using System.Collections.Generic;

namespace Message.Broker.Interfaces
{
    public interface IMessages
	{
		void Send(IMessage messageSubscriber);
		IEnumerable<IMessage> GetAll();
	}
}

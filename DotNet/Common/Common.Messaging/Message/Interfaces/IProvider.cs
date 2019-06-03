using System.Collections.Generic;

namespace Common.Messaging.Message.Interfaces
{
    public interface IProvider<out T>
	{
		IEnumerable<T> GetAll();
	}
}

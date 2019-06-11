using System.Collections.Generic;

namespace Common.Messaging.Infrastructure.Interfaces
{
    public interface IProvider<out T>
	{
		IEnumerable<T> GetAll();
	}
}

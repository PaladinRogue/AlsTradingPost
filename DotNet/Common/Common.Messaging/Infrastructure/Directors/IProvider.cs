using System.Collections.Generic;

namespace Common.Messaging.Infrastructure.Directors
{
    public interface IProvider<out T>
	{
		IEnumerable<T> GetAll();
	}
}

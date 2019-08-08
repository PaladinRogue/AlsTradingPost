using System.Collections.Generic;

namespace Messaging.Setup.Infrastructure.Directors
{
    public interface IProvider<out T>
	{
		IEnumerable<T> GetAll();
	}
}

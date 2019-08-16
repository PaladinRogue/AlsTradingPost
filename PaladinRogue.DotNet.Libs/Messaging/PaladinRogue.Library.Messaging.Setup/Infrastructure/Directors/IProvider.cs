using System.Collections.Generic;

namespace PaladinRogue.Library.Messaging.Setup.Infrastructure.Directors
{
    public interface IProvider<out T>
	{
		IEnumerable<T> GetAll();
	}
}

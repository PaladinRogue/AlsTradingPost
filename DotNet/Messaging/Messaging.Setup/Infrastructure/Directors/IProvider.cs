using System.Collections.Generic;

namespace PaladinRogue.Libray.Messaging.Setup.Infrastructure.Directors
{
    public interface IProvider<out T>
	{
		IEnumerable<T> GetAll();
	}
}

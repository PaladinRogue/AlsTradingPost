using System.Collections.Generic;

namespace Common.Resources.Interfaces
{
    public interface IProvider<out T>
	{
		IEnumerable<T> GetAll();
	}
}

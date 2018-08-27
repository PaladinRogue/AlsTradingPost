
namespace Common.Resources.Interfaces
{
    public interface IContainer<in T>
	{
		void Add(T item);
	}
}

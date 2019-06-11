
namespace Common.Messaging.Infrastructure.Interfaces
{
    public interface IContainer<in T>
	{
		void Add(T item);
	}
}

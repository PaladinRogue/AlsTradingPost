
namespace Common.Messaging.Infrastructure.Directors
{
    public interface IContainer<in T>
	{
		void Add(T item);
	}
}

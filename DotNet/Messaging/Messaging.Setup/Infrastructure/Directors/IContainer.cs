
namespace Messaging.Setup.Infrastructure.Directors
{
    public interface IContainer<in T>
	{
		void Add(T item);
	}
}

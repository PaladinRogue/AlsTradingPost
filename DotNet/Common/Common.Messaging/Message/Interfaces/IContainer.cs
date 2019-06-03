
namespace Common.Messaging.Message.Interfaces
{
    public interface IContainer<in T>
	{
		void Add(T item);
	}
}

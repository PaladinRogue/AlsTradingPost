using System.Threading.Tasks;

namespace Common.Messaging.Infrastructure.Subscribers
{
    public interface IMessageSubscriberFactory
    {
	    Task InitialiseAsync();
    }
}

using System.Threading.Tasks;

namespace Common.Messaging.Infrastructure.Handlers
{
    public interface IMessageHandlerFactory
    {
	    Task InitialiseAsync();
    }
}

using System.Threading.Tasks;

namespace Messaging.Setup.Infrastructure.Handlers
{
    public interface IMessageHandlerFactory
    {
	    Task InitialiseAsync();
    }
}

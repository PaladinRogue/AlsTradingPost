using System.Threading.Tasks;

namespace PaladinRogue.Library.Messaging.Setup.Infrastructure.Handlers
{
    public interface IMessageHandlerFactory
    {
	    Task InitialiseAsync();
    }
}

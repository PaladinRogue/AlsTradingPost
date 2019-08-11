using System.Threading.Tasks;

namespace PaladinRogue.Libray.Messaging.Setup.Infrastructure.Handlers
{
    public interface IMessageHandlerFactory
    {
	    Task InitialiseAsync();
    }
}

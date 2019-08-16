using System.Threading;
using System.Threading.Tasks;

namespace PaladinRogue.Library.Core.Setup.Infrastructure.Startup
{
    public interface IStartupTask
    {
        Task ExecuteAsync(CancellationToken cancellationToken = default);
    }
}
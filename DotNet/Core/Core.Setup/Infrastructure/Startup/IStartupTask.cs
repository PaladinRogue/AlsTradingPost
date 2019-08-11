using System.Threading;
using System.Threading.Tasks;

namespace PaladinRogue.Libray.Core.Setup.Infrastructure.Startup
{
    public interface IStartupTask
    {
        Task ExecuteAsync(CancellationToken cancellationToken = default);
    }
}
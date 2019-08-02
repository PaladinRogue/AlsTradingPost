using System.Threading;
using System.Threading.Tasks;

namespace Common.Setup.Infrastructure.Startup
{
    public interface IStartupTask
    {
        Task ExecuteAsync(CancellationToken cancellationToken = default);
    }
}
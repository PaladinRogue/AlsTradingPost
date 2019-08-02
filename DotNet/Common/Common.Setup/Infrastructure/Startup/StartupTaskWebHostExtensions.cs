using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.DependencyInjection;

namespace Common.Setup.Infrastructure.Startup
{
    public static class StartupTaskWebHostExtensions
    {
        public static async Task RunWithStartupTasksAsync(this IWebHost webHost, CancellationToken cancellationToken = default)
        {
            IEnumerable<IStartupTask> startupTasks = webHost.Services.GetServices<IStartupTask>();

            foreach (IStartupTask startupTask in startupTasks)
            {
                await startupTask.ExecuteAsync(cancellationToken);
            }

            await webHost.RunAsync(cancellationToken);
        }
    }
}
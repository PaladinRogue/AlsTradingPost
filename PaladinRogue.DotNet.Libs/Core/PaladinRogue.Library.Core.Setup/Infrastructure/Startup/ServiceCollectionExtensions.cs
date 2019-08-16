using Microsoft.Extensions.DependencyInjection;

namespace PaladinRogue.Library.Core.Setup.Infrastructure.Startup
{
    public static class ServiceCollectionExtensions
    {
        public static IServiceCollection AddStartupTask<T>(this IServiceCollection services)
            where T : class, IStartupTask
            => services.AddTransient<IStartupTask, T>();
    }
}
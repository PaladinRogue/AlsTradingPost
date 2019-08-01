using Common.Api.Routing;
using Common.ApplicationServices.Transactions;
using Microsoft.Extensions.DependencyInjection;

namespace Notifications.Setup
{
    public static class ServiceCollectionExtensions
    {

        public static IServiceCollection RegisterApplicationServices(this IServiceCollection services)
        {
            return services
                .AddTransient<ITransactionManager, TransientTransactionManager>();
        }

        public static IServiceCollection RegisterProviders(this IServiceCollection services)
        {
            return services
                .AddSingleton<IRouteProvider<bool>, DefaultRouteProvider>();
        }
    }
}
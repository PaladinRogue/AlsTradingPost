using AlsTradingPost.Api.Trader;
using Common.Api.NamingMap;
using Microsoft.Extensions.DependencyInjection;

namespace AlsTradingPost.Api
{
    public class NamingMapRegistration
    {
        public static void Register(IServiceCollection services)
        {
            services.AddSingleton<INamingMap, TraderNamingMap>();
        }
    }
}

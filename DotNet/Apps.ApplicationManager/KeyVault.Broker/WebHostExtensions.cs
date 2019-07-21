using System;
using System.Threading.Tasks;
using Common.Domain.DataProtectors;
using KeyVault.Broker.ApplicationServices;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.DependencyInjection;

namespace KeyVault.Broker
{
    public static class WebHostExtensions
    {
        public static async Task<IWebHost> CreateDataKeysAsync<T>(this IWebHost webHost) where T : struct, Enum
        {
            using (IServiceScope scope = webHost.Services.CreateScope())
            {
                IServiceProvider serviceProvider = scope.ServiceProvider;
                serviceProvider.GetRequiredService<IDataProtector>().SetDataProtector();

                ICacheDataKeyApplicationKernalService cacheDataKeyApplicationKernalService = serviceProvider.GetService<ICacheDataKeyApplicationKernalService>();

                await cacheDataKeyApplicationKernalService.CreateAndCacheAllAsync<T>();

                return webHost;
            }
        }
    }
}
using System;
using System.Threading.Tasks;
using Common.Domain.DataProtectors;
using KeyVault.ApplicationServices.SharedDataKeys.Create;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.DependencyInjection;

namespace KeyVault.Setup.Infrastructure.DataKeys
{
    public static class WebHostExtensions
    {
        public static async Task CreateSharedDataKeysAsync(this IWebHost webHost)
        {
            using (IServiceScope scope = webHost.Services.CreateScope())
            {
                IServiceProvider serviceProvider = scope.ServiceProvider;
                serviceProvider.GetRequiredService<IDataProtector>().SetDataProtector();

                ICreateSharedDataKeysApplicationKernalService createSharedDataKeysApplicationKernalService = webHost.Services.GetRequiredService<ICreateSharedDataKeysApplicationKernalService>();

                await createSharedDataKeysApplicationKernalService.ExecuteAsync();
            }
        }
    }
}
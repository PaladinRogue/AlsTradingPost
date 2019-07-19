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
            webHost.Services.GetRequiredService<IDataProtector>().SetDataProtector();

            ISharedDataKeysApplicationKernalService sharedDataKeysApplicationKernalService = webHost.Services.GetRequiredService<ISharedDataKeysApplicationKernalService>();

            await sharedDataKeysApplicationKernalService.Create();
        }
    }
}
using Common.Application.Transactions;
using Common.Domain.Concurrency;
using Common.Domain.Concurrency.Interfaces;
using Common.Domain.Concurrency.Services;
using Common.Domain.Concurrency.Services.Interfaces;
using Common.Setup.Infrastructure.Encryption;
using Common.Setup.Infrastructure.Encryption.Interfaces;
using Common.Setup.Infrastructure.Hashing;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc.ApiExplorer;
using Microsoft.Extensions.DependencyInjection;

namespace Common.Setup
{
    public class ServiceRegistration
    {
        public static void RegisterManagers(IServiceCollection services)
        {
            services.AddTransient<ITransactionManager, TransientTransactionManager>();
        }

        public static void RegisterServices(IServiceCollection services)
        {
	        services.AddSingleton<IEncryptionFactory, EncryptionFactory>();
	        services.AddSingleton<IHashFactory, HashFactory>();

            services.AddScoped(typeof(IConcurrencyQueryService<>), typeof(ConcurrencyQueryService<>));
		}

        public static void RegisterProviders(IServiceCollection services)
        {
	        services.AddSingleton<IApiDescriptionGroupCollectionProvider, ApiDescriptionGroupCollectionProvider>();
	        services.AddSingleton<IHttpContextAccessor, HttpContextAccessor>();
            services.AddSingleton<IConcurrencyVersionProvider, ConcurrencyVersionProvider>();
        }
    }
}

using Common.Application.Authorisation;
using Common.Domain.Concurrency;
using Common.Domain.Concurrency.Interfaces;
using Common.Domain.Concurrency.Services;
using Common.Domain.Concurrency.Services.Interfaces;
using Common.Domain.Services.Command;
using Common.Domain.Services.Query;
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
        public static void RegisterServices(IServiceCollection services)
        {
            services.AddSingleton<IEncryptionFactory, EncryptionFactory>();
            services.AddSingleton<IHashFactory, HashFactory>();

            services.AddScoped(typeof(IConcurrencyService<>), typeof(ConcurrencyService<>));
            services.AddScoped(typeof(ICommandService<>), typeof(CommandService<>));
            services.AddScoped(typeof(IQueryService<>), typeof(QueryService<>));

            services.AddScoped<ISecurityApplicationService, DefaultSecurityApplicationService>();
            services.AddSingleton<IAuthorisationManager, AuthorisationManager>();
        }

        public static void RegisterProviders(IServiceCollection services)
        {
	        services.AddSingleton<IResourceOwnerProvider, ResourceOwnerProvider>();
	        services.AddSingleton<IApiDescriptionGroupCollectionProvider, ApiDescriptionGroupCollectionProvider>();
	        services.AddSingleton<IHttpContextAccessor, HttpContextAccessor>();
            services.AddSingleton<IConcurrencyVersionProvider, ConcurrencyVersionProvider>();
        }
    }
}

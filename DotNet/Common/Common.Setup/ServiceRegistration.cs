using Common.ApplicationServices.Services.Query;
using Common.Authorisation;
using Common.Authorisation.Manager;
using Common.Authorisation.Policies;
using Common.Authorisation.Policies.Deny;
using Common.Resources.Encryption;
using Common.Resources.Hashing;
using Common.Setup.Infrastructure.Encryption;
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
            services.AddSingleton<IEncryptionFactory, AesEncryptionFactory>();
            services.AddSingleton<IHashFactory, Sha256HashFactory>();

            services.AddScoped(typeof(IQueryService<>), typeof(QueryService<>));

            services.AddScoped<ISecurityApplicationService, DefaultSecurityApplicationService>();
            services.AddSingleton<IAuthorisationManager, AuthorisationManager>();
        }

        public static void RegisterProviders(IServiceCollection services)
        {
	        services.AddSingleton<IResourceOwnerProvider, ResourceOwnerProvider>();
	        services.AddSingleton<IApiDescriptionGroupCollectionProvider, ApiDescriptionGroupCollectionProvider>();
	        services.AddSingleton<IHttpContextAccessor, HttpContextAccessor>();
        }

        public static void RegisterAuthorisation(IServiceCollection services)
        {
            services.AddSingleton<IAuthorisationPolicy, AlwaysDenyAuthorisationPolicy>();
        }
    }
}

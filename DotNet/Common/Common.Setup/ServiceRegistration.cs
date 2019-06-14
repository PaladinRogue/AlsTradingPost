using Common.Application.Authorisation;
using Common.Application.Authorisation.Policy;
using Common.ApplicationServices.Services.Query;
using Common.Resources.Encryption;
using Common.Setup.Infrastructure.Authorisation;
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
            services.AddSingleton<IEncryptionFactory, EncryptionFactory>();
            services.AddSingleton<IHashFactory, HashFactory>();

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

using Authentication.Application.Identity;
using Authentication.Application.Identity.Interfaces;
using Authentication.Domain.IdentityServices;
using Authentication.Domain.IdentityServices.Handlers;
using Authentication.Domain.IdentityServices.Interfaces;
using Authentication.Persistence;
using Authentication.Persistence.Interfaces;
using Authentication.Persistence.Repositories;
using Common.Api.Factories;
using Common.Api.Factories.Interfaces;
using Common.Domain.ConcurrencyServices;
using Common.Domain.ConcurrencyServices.Interfaces;
using Common.Domain.DomainEvents.Interfaces;
using Common.Domain.Providers;
using Common.Domain.Providers.Interfaces;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace Authentication.Setup
{
    public class ServiceRegistration
    {public static void RegisterServices(IConfiguration configuration, IServiceCollection services)
        {
	        services.AddSingleton<IEncryptionFactory, EncryptionFactory>();
	        services.AddSingleton<IHttpClientFactory, HttpClientFactory>();

            services.AddScoped(typeof(IConcurrencyQueryService<>), typeof(ConcurrencyQueryService<>));

			services.AddScoped<IIdentityApplicationService, IdentityApplicationService>();

			services.AddScoped<IIdentityQueryService, IdentityQueryService>();
			services.AddScoped<IIdentityCommandService, IdentityCommandService>();

			services.AddScoped<IIdentityRepository, IdentityRepository>();

            services.AddDbContext<AuthenticationDbContext>(options => options.UseSqlServer(configuration["ConnectionStrings:CheneyDb"]));
        }

        public static void RegisterProviders(IConfiguration configuration, IServiceCollection services)
        {
            services.AddSingleton<IConcurrencyVersionProvider, ConcurrencyVersionProvider>();
        }
    }
}

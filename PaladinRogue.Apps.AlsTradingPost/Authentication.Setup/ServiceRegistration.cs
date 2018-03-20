using Authentication.Persistence;
using Authentication.Persistence.Interfaces;
using Authentication.Persistence.Repositories;
using Common.Domain.ConcurrencyServices;
using Common.Domain.ConcurrencyServices.Interfaces;
using Common.Domain.Providers;
using Common.Domain.Providers.Interfaces;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace Authentication.Setup
{
    public class ServiceRegistration
    {
        public static void RegisterServices(IConfiguration configuration, IServiceCollection services)
        {

            services.AddScoped(typeof(IConcurrencyQueryService<>), typeof(ConcurrencyQueryService<>));

            services.AddScoped<IIdentityRepository, IdentityRepository>();

            services.AddDbContext<AuthenticationDbContext>(options => options.UseSqlServer(configuration["ConnectionStrings:CheneyDb"]));
        }

        public static void RegisterProviders(IConfiguration configuration, IServiceCollection services)
        {
            services.AddSingleton<IConcurrencyVersionProvider, ConcurrencyVersionProvider>();
        }
    }
}

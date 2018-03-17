using AlsTradingPost.Application.Admin;
using AlsTradingPost.Application.Admin.Interfaces;
using AlsTradingPost.Domain.AdminServices;
using AlsTradingPost.Domain.AdminServices.Interfaces;
using AlsTradingPost.Persistence;
using AlsTradingPost.Persistence.Interfaces;
using AlsTradingPost.Persistence.Repositories;
using Common.Domain.Providers;
using Common.Domain.Providers.Interfaces;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace AlsTradingPost.Setup
{
    public class ServiceRegistration
    {
        public static void RegisterServices(IConfiguration configuration, IServiceCollection services)
        {
            services.AddScoped<IAdminApplicationService, AdminApplicationService>();

            services.AddScoped<IAdminCommandService, AdminCommandService>();
            services.AddScoped<IAdminQueryService, AdminQueryService>();

            services.AddScoped<IAdminRepository, AdminRepository>();
            services.AddScoped<IPlayerRepository, PlayerRepository>();
            services.AddScoped<ICharacterRepository, CharacterRepository>();
            services.AddScoped<IItemRepository, ItemRepository>();
            services.AddScoped<IIdentityRepository, IdentityRepository>();

            services.AddDbContext<AlsTradingPostDbContext>(options => options.UseSqlServer(configuration["ConnectionStrings:CheneyDb"]));
        }

        public static void RegisterProviders(IConfiguration configuration, IServiceCollection services)
        {
            services.AddSingleton<IConcurrencyVersionProvider, ConcurrencyVersionProvider>();
        }
    }
}

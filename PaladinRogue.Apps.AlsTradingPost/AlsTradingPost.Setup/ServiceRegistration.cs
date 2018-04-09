using AlsTradingPost.Application.Admin;
using AlsTradingPost.Application.Admin.Interfaces;
using AlsTradingPost.Domain.AdminServices;
using AlsTradingPost.Domain.AdminServices.Interfaces;
using AlsTradingPost.Persistence;
using AlsTradingPost.Persistence.Interfaces;
using AlsTradingPost.Persistence.Repositories;
using AlsTradingPost.Persistence.Transactions;
using Common.Domain.ConcurrencyServices;
using Common.Domain.ConcurrencyServices.Interfaces;
using Common.Domain.Providers;
using Common.Domain.Providers.Interfaces;
using Common.Resources.Transactions;
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

            services.AddScoped(typeof(IConcurrencyQueryService<>), typeof(ConcurrencyQueryService<>));
            services.AddScoped<IAdminCommandService, AdminCommandService>();
            services.AddScoped<IAdminQueryService, AdminQueryService>();

            services.AddScoped<IAuditRepository, AuditRepository>();
            services.AddScoped<IAdminRepository, AdminRepository>();
            services.AddScoped<IPlayerRepository, PlayerRepository>();
            services.AddScoped<ICharacterRepository, CharacterRepository>();
            services.AddScoped<IItemRepository, ItemRepository>();

            services.AddDbContext<AlsTradingPostDbContext>(options => options.UseSqlServer(configuration.GetConnectionString("Default")));
            services.AddScoped<ITransactionFactory, TransactionFactory>();
        }

        public static void RegisterProviders(IConfiguration configuration, IServiceCollection services)
        {
            services.AddSingleton<IConcurrencyVersionProvider, ConcurrencyVersionProvider>();
        }
    }
}

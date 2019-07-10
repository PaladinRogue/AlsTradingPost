using ApplicationManager.Persistence;
using Common.ApplicationServices.Transactions;
using Common.ApplicationServices.WebRequests;
using Common.Setup.Infrastructure.WebRequests;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Diagnostics;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Persistence.EntityFramework.Infrastructure.Transactions;

namespace ReverseProxy.Setup
{
    public class ServiceRegistration
    {
        public static void RegisterServices(IServiceCollection services)
        {
            services.AddSingleton<IHttpClientFactory, HttpClientFactory>();
        }

        public static void RegisterPersistenceServices(IConfiguration configuration, IServiceCollection services)
        {
	        services.AddEntityFrameworkSqlServer().AddOptions()
		        .AddDbContext<ApplicationManagerDbContext>(options =>
			        options.ConfigureWarnings(warnings => warnings.Throw(RelationalEventId.QueryClientEvaluationWarning))
				        .UseSqlServer(configuration.GetConnectionString("Default")));
	        services.AddScoped<DbContext>(sp => sp.GetRequiredService<ApplicationManagerDbContext>());
	        services.AddScoped<ITransactionManager, EntityFrameworkTransactionManager>();
        }
    }
}
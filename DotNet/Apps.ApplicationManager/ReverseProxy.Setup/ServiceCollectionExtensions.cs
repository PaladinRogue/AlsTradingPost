using Common.ApplicationServices.Caching;
using Common.ApplicationServices.Transactions;
using Common.Domain.Persistence;
using Common.Setup.Infrastructure.Caching;
using FluentValidation;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Diagnostics;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Persistence.EntityFramework.Infrastructure.Transactions;
using Persistence.EntityFramework.Repositories;
using ReverseProxy.ApplicationServices.Applications;
using ReverseProxy.ApplicationServices.Applications.Caching;
using ReverseProxy.ApplicationServices.Applications.Register;
using ReverseProxy.Domain.Applications;
using ReverseProxy.Domain.Applications.Change;
using ReverseProxy.Domain.Applications.Create;
using ReverseProxy.Domain.Applications.Persistence;
using ReverseProxy.Persistence;
using ReverseProxy.Persistence.Applications;
using ReverseProxy.Setup.Infrastructure.Caching;

namespace ReverseProxy.Setup
{
    public static class ServiceCollectionExtensions
    {
        public static IServiceCollection RegisterApplicationServices(this IServiceCollection services)
        {
            return services
                .AddScoped<IRegisterApplicationKernalService, RegisterApplicationKernalService>()
                .AddScoped<IApplicationKernalService, ApplicationKernalService>();
        }

        public static IServiceCollection RegisterValidators(this IServiceCollection services)
        {
            ValidatorOptions.LanguageManager.Enabled = false;

            return services
                .AddScoped<IValidator<ChangeApplicationDdto>, ChangeApplicationValidator>()
                .AddScoped<IValidator<CreateApplicationDdto>, CreateApplicationValidator>();
        }

        public static IServiceCollection RegisterDomainServices(this IServiceCollection services)
        {
            return services
                .AddScoped<IChangeApplicationCommand, ChangeApplicationCommand>()
                .AddScoped<ICreateApplicationCommand, CreateApplicationCommand>();
        }

        public static IServiceCollection RegisterPersistenceServices(
            this IServiceCollection services,
            IConfiguration configuration)
        {
            services.AddScoped<ICommandRepository<Application>, CommandRepository<Application>>();

            services.AddSingletonCache<IApplicationQueryRepository, ApplicationQueryRepository, ICacheDecorator<string, Application>, ApplicationQueryRepositoryCacheDecorator, GatewayCacheService>();

            services.AddEntityFrameworkSqlServer().AddOptions()
                .AddDbContext<ReverseProxyDbContext>(options =>
                    options.UseLazyLoadingProxies()
                        .ConfigureWarnings(warnings => warnings.Throw(RelationalEventId.QueryClientEvaluationWarning))
                        .UseSqlServer(configuration.GetConnectionString("Default")));
            services.AddScoped<DbContext>(sp => sp.GetRequiredService<ReverseProxyDbContext>());
            services.AddScoped<ITransactionManager, EntityFrameworkTransactionManager>();

            return services;
        }

        public static IServiceCollection RegisterProviders(this IServiceCollection services)
        {
            return services;
        }
    }
}
using Common.ApplicationServices.Transactions;
using Common.Domain.Persistence;
using FluentValidation;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Diagnostics;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Persistence.EntityFramework.Infrastructure.Transactions;
using Persistence.EntityFramework.Repositories;
using ReverseProxy.ApplicationServices.Applications;
using ReverseProxy.ApplicationServices.Applications.Register;
using ReverseProxy.Domain.Applications;
using ReverseProxy.Domain.Applications.Change;
using ReverseProxy.Domain.Applications.Create;
using ReverseProxy.Persistence;
using ReverseProxy.Setup.Infrastructure.Applications;

namespace ReverseProxy.Setup
{
    public static class ServiceCollectionExtensions
    {
        public static IServiceCollection RegisterApplicationServices(this IServiceCollection services)
        {
            return services
                .AddScoped<IRegisterApplicationKernalService, RegisterApplicationKernalService>()
                .AddScoped<IApplicationKernalService, ApplicationKernalService>()
                .AddSingleton<IApplicationCache, InMemoryApplicationCache>();
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

            services.AddScoped<IQueryRepository<Application>, QueryRepository<Application>>();

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
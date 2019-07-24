using Common.ApplicationServices.Caching;
using Common.ApplicationServices.Transactions;
using Common.Domain.Persistence;
using Common.Setup.Infrastructure.Caching;
using FluentValidation;
using Gateway.ApplicationServices.Applications;
using Gateway.ApplicationServices.Applications.Caching;
using Gateway.ApplicationServices.Applications.Register;
using Gateway.Domain.Applications;
using Gateway.Domain.Applications.Change;
using Gateway.Domain.Applications.Create;
using Gateway.Domain.Applications.Persistence;
using Gateway.Persistence;
using Gateway.Persistence.Applications;
using Gateway.Setup.Infrastructure.Caching;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Diagnostics;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Persistence.EntityFramework.Infrastructure.Transactions;
using Persistence.EntityFramework.Repositories;

namespace Gateway.Setup
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

        public static IServiceCollection RegisterDomainCommands(this IServiceCollection services)
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
                .AddDbContext<GatewayDbContext>(options =>
                    options.UseLazyLoadingProxies()
                        .ConfigureWarnings(warnings => warnings.Throw(RelationalEventId.QueryClientEvaluationWarning))
                        .UseSqlServer(configuration.GetConnectionString("Default")));
            services.AddScoped<DbContext>(sp => sp.GetRequiredService<GatewayDbContext>());
            services.AddScoped<ITransactionManager, EntityFrameworkTransactionManager>();

            return services;
        }
    }
}
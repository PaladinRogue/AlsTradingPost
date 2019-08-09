using Common.Api.Routing;
using Common.Application.Caching;
using Common.Application.Transactions;
using Common.Domain.Persistence;
using Common.Setup.Infrastructure.Authorisation;
using Common.Setup.Infrastructure.Caching;
using FluentValidation;
using Gateway.Application.Applications;
using Gateway.Application.Applications.Caching;
using Gateway.Application.Applications.Register;
using Gateway.Domain.Applications;
using Gateway.Domain.Applications.Change;
using Gateway.Domain.Applications.Create;
using Gateway.Domain.Applications.Persistence;
using Gateway.Persistence;
using Gateway.Persistence.Applications;
using Gateway.Setup.Infrastructure.Caching;
using Gateway.Setup.Infrastructure.Routing;
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
        public static IServiceCollection AddApplicationDomain(this IServiceCollection services)
        {
            return services
                .AddSecureApplicationService<IApplicationApplicationService, ApplicationApplicationService, ApplicationApplicationServiceSecurityDecorator>()
                .AddScoped<IRegisterApplicationKernalService, RegisterApplicationKernalService>()
                .AddScoped<IApplicationKernalService, ApplicationKernalService>()
                .AddScoped<IChangeApplicationCommand, ChangeApplicationCommand>()
                .AddScoped<ICreateApplicationCommand, CreateApplicationCommand>()
                .AddScoped<IValidator<ChangeApplicationDdto>, ChangeApplicationValidator>()
                .AddScoped<IValidator<CreateApplicationDdto>, CreateApplicationValidator>()
                .AddSingletonCache<IApplicationQueryRepository, ApplicationQueryRepository, ICacheDecorator<string, Domain.Applications.Application>, ApplicationQueryRepositoryCacheDecorator, GatewayCacheService>()
                .AddScoped<IQueryRepository<Domain.Applications.Application>, QueryRepository<Domain.Applications.Application>>()
                .AddScoped<ICommandRepository<Domain.Applications.Application>, CommandRepository<Domain.Applications.Application>>();
        }

        public static IServiceCollection AddGatewayPersistence(
            this IServiceCollection services,
            IConfiguration configuration)
        {
            return services.AddEntityFrameworkSqlServer().AddOptions()
                .AddDbContext<GatewayDbContext>(options =>
                    options.UseLazyLoadingProxies()
                        .ConfigureWarnings(warnings => warnings.Throw(RelationalEventId.QueryClientEvaluationWarning))
                        .UseSqlServer(configuration.GetConnectionString("Default")))
                .AddScoped<DbContext>(sp => sp.GetRequiredService<GatewayDbContext>())
                .AddScoped<ITransactionManager, EntityFrameworkTransactionManager>();
        }

        public static IServiceCollection UseGatewayRouteProvider(this IServiceCollection services)
        {
            return services
                .AddScoped<IRouteProvider<bool>, GatewayRouteProvider>();
        }
    }
}
using FluentValidation;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Diagnostics;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using PaladinRogue.Gateway.Application.Applications;
using PaladinRogue.Gateway.Application.Applications.Caching;
using PaladinRogue.Gateway.Application.Applications.Register;
using PaladinRogue.Gateway.Domain.Applications.Change;
using PaladinRogue.Gateway.Domain.Applications.Create;
using PaladinRogue.Gateway.Domain.Applications.Persistence;
using PaladinRogue.Gateway.Persistence;
using PaladinRogue.Gateway.Persistence.Applications;
using PaladinRogue.Gateway.Setup.Infrastructure.Caching;
using PaladinRogue.Gateway.Setup.Infrastructure.Routing;
using PaladinRogue.Library.Authorisation.Setup.Infrastructure.Authorisation;
using PaladinRogue.Library.Core.Application.Caching;
using PaladinRogue.Library.Core.Application.Transactions;
using PaladinRogue.Library.Core.Domain.Persistence;
using PaladinRogue.Library.Core.Setup.Infrastructure.Routing;
using PaladinRogue.Library.Persistence.EntityFramework.Repositories;
using PaladinRogue.Library.Persistence.Setup.Infrastructure.Caching;
using PaladinRogue.Library.Persistence.Setup.Infrastructure.Transactions;

namespace PaladinRogue.Gateway.Setup
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
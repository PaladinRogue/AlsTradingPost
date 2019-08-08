using System;
using Common.ApplicationServices.Caching;
using Common.Domain.Aggregates;
using Common.Domain.Persistence;
using Microsoft.Extensions.DependencyInjection;
using Persistence.EntityFramework.Repositories;

namespace Common.Setup.Infrastructure.Caching
{
    public static class RegisterCacheExtensions
    {
        public static IServiceCollection AddSingletonQueryRepositoryCache<T, TCacheService>(this IServiceCollection services)
            where T : class, IAggregateRoot
            where TCacheService : class, ICacheService
        {
            services.AddScoped<QueryRepository<T>>();
            services.AddSingleton<TCacheService>();

            services.AddScoped<IQueryRepository<T>>(sp =>
                Activator.CreateInstance(typeof(QueryRepositoryCacheDecorator<T>), sp.GetRequiredService<QueryRepository<T>>(), sp.GetRequiredService<TCacheService>()) as
                    QueryRepositoryCacheDecorator<T>
            );

            services.AddScoped<ICacheDecorator<Guid, T>>(sp =>
                Activator.CreateInstance(typeof(QueryRepositoryCacheDecorator<T>), sp.GetRequiredService<QueryRepository<T>>(), sp.GetRequiredService<TCacheService>()) as
                    QueryRepositoryCacheDecorator<T>
            );

            return services;
        }

        public static IServiceCollection AddSingletonCache<TIService, TService, TICacheDecorator, TServiceCacheDecorator, TCacheService>(
            this IServiceCollection services,
            ServiceLifetime serviceLifetime = ServiceLifetime.Scoped)
            where TIService : class
            where TService : class, TIService
            where TICacheDecorator : class
            where TServiceCacheDecorator : class, TIService, TICacheDecorator
            where TCacheService : class, ICacheService
        {
            services.AddSingleton<TCacheService>();

            services.AddScoped<TICacheDecorator>(sp =>
                Activator.CreateInstance(typeof(TServiceCacheDecorator), sp.GetRequiredService<TService>(), sp.GetRequiredService<TCacheService>()) as TServiceCacheDecorator
            );

            return AddServiceCache<TIService, TService, TServiceCacheDecorator, TCacheService>(services, serviceLifetime);
        }

        public static IServiceCollection AddScopedCache<TIService, TService, TServiceCacheDecorator, TCacheService>(this IServiceCollection services)
            where TIService : class
            where TService : class, TIService
            where TServiceCacheDecorator : class, TIService
            where TCacheService : class, ICacheService
        {
            services.AddScoped<TCacheService>();

            return AddServiceCache<TIService, TService, TServiceCacheDecorator, TCacheService>(services, ServiceLifetime.Scoped);
        }

        private static IServiceCollection AddServiceCache<TIService, TService, TServiceCacheDecorator, TCacheService>(IServiceCollection services, ServiceLifetime serviceLifetime)
            where TIService : class
            where TService : class, TIService
            where TServiceCacheDecorator : class, TIService
            where TCacheService : class, ICacheService
        {
            services.Add(new ServiceDescriptor(typeof(TService), typeof(TService), serviceLifetime));

            services.Add(
                new ServiceDescriptor(
                    typeof(TIService),
                    sp =>
                        Activator.CreateInstance(typeof(TServiceCacheDecorator), sp.GetRequiredService<TService>(), sp.GetRequiredService<TCacheService>()) as TServiceCacheDecorator
                    , serviceLifetime)
            );

            return services;
        }
    }
}
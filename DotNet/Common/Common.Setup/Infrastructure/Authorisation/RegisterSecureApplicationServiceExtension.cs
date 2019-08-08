using System;
using Authorisation.Application.ApplicationServices;
using Microsoft.Extensions.DependencyInjection;

namespace Common.Setup.Infrastructure.Authorisation
{
    public static class RegisterSecureApplicationServiceExtension
    {
        public static IServiceCollection AddSecureApplicationService<TIApplicationService, TApplicationService, TApplicationServiceSecurityDecorator>(this IServiceCollection services)
            where TIApplicationService : class
            where TApplicationService : class, TIApplicationService
            where TApplicationServiceSecurityDecorator : class, TIApplicationService
        {
            services.AddScoped<TApplicationService>();

            services.AddScoped<TIApplicationService>(sp =>
                Activator.CreateInstance(typeof(TApplicationServiceSecurityDecorator), sp.GetRequiredService<ISecurityApplicationService>(), sp.GetRequiredService<TApplicationService>()) as TApplicationServiceSecurityDecorator
            );

            return services;
        }
    }
}
using System;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Options;

namespace PaladinRogue.Libray.Core.Setup.Infrastructure.Options
{
    public static class ServiceProviderExtensions
    {
        public static T GetRequiredOptions<T>(this IServiceProvider serviceProvider) where T : class, new()
        {
            IOptions<T> settingsAccessor = serviceProvider.GetRequiredService<IOptions<T>>();

            return settingsAccessor.Value;
        }
    }
}
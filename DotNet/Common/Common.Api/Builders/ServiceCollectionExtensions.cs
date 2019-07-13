using Common.Api.Builders.Resource;
using Common.Api.Links;
using Microsoft.Extensions.DependencyInjection;

namespace Common.Api.Builders
{
    public static class ServiceCollectionExtensions
    {
        public static IServiceCollection UseDefaultResourceBuilders(this IServiceCollection services)
        {
            return services.AddSingleton<ILinkBuilder, DefaultLinkBuilder>()
                .AddSingleton<IResourceBuilder, DefaultResourceBuilder>()
                .AddSingleton<IPagingLinkBuilder, DefaultPagingLinkBuilder>()
                .AddSingleton<ISortingLinkBuilder, DefaultSortingLinkBuilder>();
        }
    }
}
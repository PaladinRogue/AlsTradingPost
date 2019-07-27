using Common.Api.Builders.Resource;
using Common.Api.Links;
using Microsoft.Extensions.DependencyInjection;

namespace Common.Api.Builders
{
    public static class ServiceCollectionExtensions
    {
        public static IServiceCollection UseDefaultResourceBuilders(this IServiceCollection services)
        {
            return services
                .AddScoped<ILinkBuilder, DefaultLinkBuilder>()
                .AddScoped<IResourceBuilder, DefaultResourceBuilder>()
                .AddScoped<IPagingLinkBuilder, DefaultPagingLinkBuilder>()
                .AddScoped<ISortingLinkBuilder, DefaultSortingLinkBuilder>()
                .AddScoped<ILinkFactory, DefaultLinkFactory>();
        }
    }
}
using Microsoft.Extensions.DependencyInjection;
using PaladinRogue.Library.Core.Api.Builders.Resource;
using PaladinRogue.Library.Core.Api.Links;

namespace PaladinRogue.Library.Core.Api.Builders
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
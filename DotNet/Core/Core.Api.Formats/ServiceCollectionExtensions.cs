using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Options;
using PaladinRogue.Libray.Core.Api.Exceptions;
using PaladinRogue.Libray.Core.Api.Formats.JsonV1;
using PaladinRogue.Libray.Core.Api.Formats.JsonV1.Filtering;
using PaladinRogue.Libray.Core.Api.Formats.JsonV1.Formatters;
using PaladinRogue.Libray.Core.Api.Formats.JsonV1.Paging;
using PaladinRogue.Libray.Core.Api.Formats.JsonV1.Sorting;
using PaladinRogue.Libray.Core.Api.Validation;

namespace PaladinRogue.Libray.Core.Api.Formats
{
    public static class ServiceCollectionExtensions
    {
        public static IServiceCollection UseJsonV1Format(this IServiceCollection services)
        {
            return services.AddSingleton<IConfigureOptions<MvcOptions>, ConfigureJsonV1MvcOptions>()
                .Configure<MvcOptions>(options =>
                {
                    options.RespectBrowserAcceptHeader = true;
                    options.ReturnHttpNotAcceptable = true;

                    options.Conventions.Add(new QueryStringSortConvention());
                    options.ModelBinderProviders.Insert(0, new QueryStringFilterModelBinderProvider());
                    options.ModelBinderProviders.Insert(0, new QueryStringSortModelBinderProvider());
                    options.ModelBinderProviders.Insert(0, new QueryStringPageSizeModelBinderProvider());
                    options.ModelBinderProviders.Insert(0, new QueryStringPageOffsetModelBinderProvider());
                })
                .AddSingleton<IApplicationErrorFormatter<IFormattedError>, JsonV1ApplicationErrorFormatter>()
                .AddSingleton<IValidationErrorFormatter<IFormattedError>, JsonV1ValidationErrorFormatter>();
        }
    }
}
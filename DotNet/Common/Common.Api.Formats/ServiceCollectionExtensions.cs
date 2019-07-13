using Common.Api.Exceptions;
using Common.Api.Formats.JsonV1;
using Common.Api.Formats.JsonV1.Filtering;
using Common.Api.Formats.JsonV1.Formatters;
using Common.Api.Formats.JsonV1.Paging;
using Common.Api.Formats.JsonV1.Sorting;
using Common.Api.Validation;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Options;

namespace Common.Api.Formats
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
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
    public static class FormatRegistration
    {
        public static void ConfigureJsonV1Format(IServiceCollection services)
        {
            services.AddSingleton<IConfigureOptions<MvcOptions>, ConfigureJsonV1MvcOptions>();

            services.Configure<MvcOptions>(options =>
            {
                options.RespectBrowserAcceptHeader = true;
                options.ReturnHttpNotAcceptable = true;

                options.Conventions.Add(new QueryStringSortConvention());
                options.ModelBinderProviders.Insert(0, new QueryStringFilterModelBinderProvider());
                options.ModelBinderProviders.Insert(0, new QueryStringSortModelBinderProvider());
                options.ModelBinderProviders.Insert(0, new QueryStringPageSizeModelBinderProvider());
                options.ModelBinderProviders.Insert(0, new QueryStringPageOffsetModelBinderProvider());
            });

            services.AddSingleton<IApplicationErrorFormatter<IFormattedError>, JsonV1ApplicationErrorFormatter>();
            services.AddSingleton<IValidationErrorFormatter<IFormattedError>, JsonV1ValidationErrorFormatter>();
        }
    }
}

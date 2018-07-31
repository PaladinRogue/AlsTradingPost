﻿using System.Linq;
using Common.Api.Exceptions;
using Common.Api.Formats.JsonV1.Formatters;
using Common.Api.Formats.JsonV1.Paging;
using Common.Api.Formats.JsonV1.Sorting;
using Common.Api.Formats.JsonV1.Filtering;
using Common.Api.Validation;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Formatters;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Net.Http.Headers;

namespace Common.Api.Formats.JsonV1
{
    public static class FormatRegistration
    {
        private const string JsonV1MediaType = "application/vnd.api+json";

        public static void ConfigureJsonV1Format(IServiceCollection services)
        {
            services.Configure<MvcOptions>(options =>
            {
                options.RespectBrowserAcceptHeader = true;
                options.ReturnHttpNotAcceptable = true;

                JsonInputFormatter jsonInputFormatter = options.InputFormatters.OfType<JsonInputFormatter>().First();
                jsonInputFormatter.SupportedMediaTypes.Clear();
                jsonInputFormatter.SupportedMediaTypes.Add(MediaTypeHeaderValue.Parse(JsonV1MediaType));

                JsonOutputFormatter jsonOutputFormatter =
                    options.OutputFormatters.OfType<JsonOutputFormatter>().First();
                jsonOutputFormatter.SupportedMediaTypes.Clear();
                jsonOutputFormatter.SupportedMediaTypes.Add(MediaTypeHeaderValue.Parse(JsonV1MediaType));


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

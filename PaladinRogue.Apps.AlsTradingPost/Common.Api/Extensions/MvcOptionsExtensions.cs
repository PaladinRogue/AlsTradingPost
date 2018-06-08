﻿using System;
using System.Buffers;
using Common.Api.Authentication;
using Common.Api.Concurrency;
using Common.Api.Validation;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Formatters;
using Microsoft.Extensions.DependencyInjection;
using Newtonsoft.Json;
using Newtonsoft.Json.Serialization;

namespace Common.Api.Extensions
{
    public static class MvcOptionsExtensions
    {
        public static MvcOptions UseCamelCaseJsonOutputFormatter<T>(this MvcOptions options) where T : JsonOutputFormatter, IOutputFormatter
        {
            // Remove any json output formatter 
            options.OutputFormatters.RemoveType<JsonOutputFormatter>();

            // Add custom json output formatter 
            JsonSerializerSettings jsonSerializerSettings = new JsonSerializerSettings
            {
                ContractResolver = new CamelCasePropertyNamesContractResolver(),
                NullValueHandling = NullValueHandling.Ignore
            };

            T customJsonOutputFormatter = (T)Activator.CreateInstance(typeof(T), jsonSerializerSettings, ArrayPool<char>.Shared);

            options.OutputFormatters.Add(customJsonOutputFormatter);

            return options;
        }

        public static MvcOptions UseConcurrencyFilter(this MvcOptions options)
        {
            options.Filters.Add(new ConcurrencyActionFilter());

            return options;
        }

        public static MvcOptions UseValidationExceptionFilter(this MvcOptions options)
        {
            options.Filters.Add(new BusinessValidationRuleApplicationExceptionFilter());

            return options;
        }

        public static MvcOptions UseAppAccessAuthorizeFilter(this MvcOptions options)
        {
            options.Conventions.Add(new AuthorizeAppAccessControllerFilter());

            return options;
        }

        public static MvcOptions RequireHttps(this MvcOptions options)
        {
            options.Filters.Add(new RequireHttpsAttribute());

            return options;
        }
    }
}

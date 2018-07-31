using Common.Api.Authentication;
using Common.Api.Concurrency;
using Common.Api.Exceptions;
using Common.Api.Filters;
using Common.Api.Validation;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.DependencyInjection;

namespace Common.Api.Extensions
{
    public static class MvcOptionsExtensions
    {
        public static MvcOptions UseConcurrencyFilter(this MvcOptions options)
        {
            options.Filters.Add<ConcurrencyActionFilter>();

            return options;
        }

        public static MvcOptions UseContentTypeFilter(this MvcOptions options)
        {
            options.Filters.Add<ContentTypeResourceFilter>();

            return options;
        }

        public static MvcOptions UseBusinessExceptionFilter(this MvcOptions options)
        {
            options.Filters.Add<BusinessApplicationExceptionFilter>();

            return options;
        }

        public static MvcOptions UseValidationExceptionFilter(this MvcOptions options)
        {
            options.Filters.Add<BusinessValidationRuleApplicationExceptionFilter>();

            return options;
        }

        public static MvcOptions UseAppAccessAuthorizeFilter(this MvcOptions options)
        {
            options.Conventions.Add(new AuthorizeAppAccessControllerModelConvention());

            return options;
        }

        public static MvcOptions RequireHttps(this MvcOptions options)
        {
            options.Filters.Add<RequireHttpsAttribute>();

            return options;
        }
    }
}

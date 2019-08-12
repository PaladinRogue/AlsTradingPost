using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.DependencyInjection;
using PaladinRogue.Library.Core.Api.Authorisation;
using PaladinRogue.Library.Core.Api.Concurrency;
using PaladinRogue.Library.Core.Api.Exceptions;
using PaladinRogue.Library.Core.Api.Validation;

namespace PaladinRogue.Library.Core.Api.Extensions
{
    public static class MvcOptionsExtensions
    {
        public static MvcOptions UseConcurrencyFilter(this MvcOptions options)
        {
            options.Filters.Add<ConcurrencyActionFilter>();

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
            options.Conventions.Add(new AuthoriseAppAccessControllerModelConvention());

            return options;
        }

        public static MvcOptions RequireHttps(this MvcOptions options)
        {
            options.Filters.Add<RequireHttpsAttribute>();

            return options;
        }
    }
}

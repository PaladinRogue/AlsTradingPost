using Common.Application.Exceptions;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;

namespace Common.Api.Validation
{
    public class ValidationExceptionActionFilter : ExceptionFilterAttribute
    {
        public override void OnException(ExceptionContext context)
        {
            if (context.Exception is BusinessValidationRuleApplicationException businessValidationRuleApplicationException )
            {
                context.ExceptionHandled = true;
                context.Result = new BadRequestObjectResult(businessValidationRuleApplicationException.ValidationResult);
            }
        }
    }
}
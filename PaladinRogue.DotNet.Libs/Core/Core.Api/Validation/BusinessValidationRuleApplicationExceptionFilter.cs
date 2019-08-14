using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;
using PaladinRogue.Library.Core.Api.Exceptions;
using PaladinRogue.Library.Core.Application.Exceptions;

namespace PaladinRogue.Library.Core.Api.Validation
{
    public class BusinessValidationRuleApplicationExceptionFilter : IExceptionFilter
    {
        private readonly IValidationErrorFormatter<IFormattedError> _validationErrorFormatter;

        public BusinessValidationRuleApplicationExceptionFilter(IValidationErrorFormatter<IFormattedError> validationErrorFormatter)
        {
            _validationErrorFormatter = validationErrorFormatter;
        }

        public void OnException(ExceptionContext context)
        {
            if (context.Exception is BusinessValidationRuleApplicationException businessValidationRuleApplicationException )
            {
                context.ExceptionHandled = true;
                context.Result = new BadRequestObjectResult(_validationErrorFormatter.Format(businessValidationRuleApplicationException.ValidationResult));
            }
        }
    }
}
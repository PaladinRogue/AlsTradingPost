using System;
using Common.Application.Validation;

namespace Common.Application.Exceptions
{
    public class BusinessValidationRuleApplicationException : Exception 
    {
        public ValidationResult ValidationResult { get; }

        public BusinessValidationRuleApplicationException(ValidationResult validationResult)
        {
            ValidationResult = validationResult;
        }
    }
}

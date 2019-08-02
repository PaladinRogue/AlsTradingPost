using System;
using Common.Domain.Validation;

namespace Common.ApplicationServices.Exceptions
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

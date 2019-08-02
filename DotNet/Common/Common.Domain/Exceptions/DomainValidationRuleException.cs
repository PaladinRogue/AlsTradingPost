using System;
using Common.Domain.Validation;

namespace Common.Domain.Exceptions
{
    public class DomainValidationRuleException : Exception 
    {
        public ValidationResult ValidationResult { get; }

        public DomainValidationRuleException(ValidationResult validationResult)
        {
            ValidationResult = validationResult;
        }
    }
}

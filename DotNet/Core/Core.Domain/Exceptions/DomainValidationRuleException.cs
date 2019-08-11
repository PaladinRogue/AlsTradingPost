using System;
using PaladinRogue.Libray.Core.Domain.Validation;

namespace PaladinRogue.Libray.Core.Domain.Exceptions
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

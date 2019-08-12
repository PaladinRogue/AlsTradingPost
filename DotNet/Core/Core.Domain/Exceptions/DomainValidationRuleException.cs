using System;
using PaladinRogue.Library.Core.Domain.Validation;

namespace PaladinRogue.Library.Core.Domain.Exceptions
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

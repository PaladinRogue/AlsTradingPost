using System;
using PaladinRogue.Libray.Core.Domain.Validation;

namespace PaladinRogue.Libray.Core.Application.Exceptions
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

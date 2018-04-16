using System;
using Common.Application.Validation;

namespace Common.Application.Exceptions
{
    public class ValidationAppException : Exception 
    {
        public ValidationResult ValidationResult { get; set; }

        public ValidationAppException(ValidationResult validationResult)
        {
            ValidationResult = validationResult;
        }
    }
}

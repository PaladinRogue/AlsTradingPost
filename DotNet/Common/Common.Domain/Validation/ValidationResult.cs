using System.Collections.Generic;

namespace Common.Domain.Validation
{
    public class ValidationResult
    {
        public IEnumerable<PropertyValidationError> PropertyValidationErrors { get; set; }
    }
}

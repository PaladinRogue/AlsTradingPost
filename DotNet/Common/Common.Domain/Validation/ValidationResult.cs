using System.Collections.Generic;
using Common.Domain.Sorting;

namespace Common.Domain.Validation
{
    public class ValidationResult
    {
        public IEnumerable<PropertyValidationError> PropertyValidationErrors { get; set; }
    }
}

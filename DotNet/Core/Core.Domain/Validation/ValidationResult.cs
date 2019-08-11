using System.Collections.Generic;

namespace PaladinRogue.Libray.Core.Domain.Validation
{
    public class ValidationResult
    {
        public IEnumerable<PropertyValidationError> PropertyValidationErrors { get; set; }
    }
}

using System.Collections.Generic;

namespace PaladinRogue.Library.Core.Domain.Validation
{
    public class ValidationResult
    {
        public IEnumerable<PropertyValidationError> PropertyValidationErrors { get; set; }
    }
}

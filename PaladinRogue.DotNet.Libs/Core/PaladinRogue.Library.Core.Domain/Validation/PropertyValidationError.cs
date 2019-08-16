using System.Collections.Generic;

namespace PaladinRogue.Library.Core.Domain.Validation
{
    public class PropertyValidationError
    {
        public string PropertyName { get; set; }

        public IEnumerable<ValidationError> ValidationErrors { get; set; }
    }
}

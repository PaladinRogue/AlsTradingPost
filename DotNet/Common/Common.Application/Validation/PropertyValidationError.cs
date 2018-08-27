using System.Collections.Generic;

namespace Common.Application.Validation
{
    public class PropertyValidationError
    {
        public string PropertyName { get; set; }

        public IEnumerable<ValidationError> ValidationErrors { get; set; }
    }
}

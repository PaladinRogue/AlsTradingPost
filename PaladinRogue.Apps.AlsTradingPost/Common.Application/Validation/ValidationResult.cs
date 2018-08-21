using System.Collections.Generic;
using Common.Application.Sorting;
using Common.Domain.Sorting;

namespace Common.Application.Validation
{
    public class ValidationResult
    {
        public IEnumerable<PropertyValidationError> PropertyValidationErrors { get; set; }

        public static ValidationResult CreateFromException(PropertyNotSortableException exception)
        {
            return new ValidationResult
            {
                PropertyValidationErrors = new List<PropertyValidationError>
                {
                    new PropertyValidationError
                    {
                        PropertyName = nameof(ISortAdto.Sort),
                        ValidationErrors = new List<ValidationError>
                        {
                            new ValidationError
                            {
                                ValidationErrorCode = ValidationErrorCodes.InvalidSort,
                                ValidationMeta = new Dictionary<string, object>
                                {
                                    {ValidationProperties.ProprtyValue, exception.PropertyName}
                                }
                            }
                        }
                    }
                }
            };
        }
    }
}

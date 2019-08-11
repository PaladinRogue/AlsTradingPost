using System.Collections.Generic;
using PaladinRogue.Libray.Core.Domain.Sorting;
using PaladinRogue.Libray.Core.Domain.Validation;

namespace PaladinRogue.Libray.Core.Application.Sorting
{
    public static class PropertyNotSortableExceptionExtentions
    {
        public static ValidationResult CreateFromException(this PropertyNotSortableException exception)
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

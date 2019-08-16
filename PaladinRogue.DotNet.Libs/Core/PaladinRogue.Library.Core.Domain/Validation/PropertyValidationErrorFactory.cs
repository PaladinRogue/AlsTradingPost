using System.Collections.Generic;

namespace PaladinRogue.Library.Core.Domain.Validation
{
    public static class PropertyValidationErrorFactory
    {
        public static PropertyValidationError Create(string fieldName, string fieldValue, string errorCode)
        {
            return new PropertyValidationError
            {
                PropertyName = fieldName,
                ValidationErrors = new List<ValidationError>
                {
                    new ValidationError
                    {
                        ValidationErrorCode = errorCode,
                        ValidationMeta = new Dictionary<string, object>
                        {
                            { ValidationMeta.PropertyName, fieldName },
                            { ValidationMeta.PropertyValue, fieldValue }
                        }
                    }
                }
            };
        }
    }
}
using System.Linq;
using Common.Api.Formats.JsonV1.Formats;
using Common.Api.Validation;
using Common.Domain.Validation;
using Common.Resources.Extensions;

namespace Common.Api.Formats.JsonV1.Formatters
{
    public class JsonV1ValidationErrorFormatter : IValidationErrorFormatter<FormattedError>
    {
        public FormattedError Format(ValidationResult validationResult)
        {
            return new FormattedError
            {
                Errors = validationResult.PropertyValidationErrors.Select(e => new Error
                {
                    Code = e.PropertyName.ToCamelCase(),
                    Meta = e.ValidationErrors.ToDictionary(ve => ve.ValidationErrorCode, ve => ve.ValidationMeta as object)
                })
            };
        }
    }
}

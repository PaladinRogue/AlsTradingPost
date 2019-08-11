using System.Linq;
using PaladinRogue.Libray.Core.Api.Formats.JsonV1.Formats;
using PaladinRogue.Libray.Core.Api.Validation;
using PaladinRogue.Libray.Core.Common.Extensions;
using PaladinRogue.Libray.Core.Domain.Validation;

namespace PaladinRogue.Libray.Core.Api.Formats.JsonV1.Formatters
{
    public class JsonV1ValidationErrorFormatter : IValidationErrorFormatter<FormattedError>
    {
        public FormattedError Format(ValidationResult validationResult)
        {
            return FormattedError.Create(validationResult.PropertyValidationErrors.Select(e => new Error
            {
                Code = e.PropertyName.ToCamelCase(),
                Meta = e.ValidationErrors.ToDictionary(ve => ve.ValidationErrorCode, ve => ve.ValidationMeta as object)
            }));
        }
    }
}

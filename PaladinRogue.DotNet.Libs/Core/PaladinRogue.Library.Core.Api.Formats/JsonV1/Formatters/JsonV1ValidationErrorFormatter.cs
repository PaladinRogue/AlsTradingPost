using System.Linq;
using PaladinRogue.Library.Core.Api.Formats.JsonV1.Formats;
using PaladinRogue.Library.Core.Api.Validation;
using PaladinRogue.Library.Core.Common.Extensions;
using PaladinRogue.Library.Core.Domain.Validation;

namespace PaladinRogue.Library.Core.Api.Formats.JsonV1.Formatters
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

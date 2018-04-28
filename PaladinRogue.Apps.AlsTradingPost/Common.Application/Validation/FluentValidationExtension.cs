using System.Linq;
using Common.Application.Exceptions;
using Common.Resources.Extensions;
using FluentValidation;

namespace Common.Application.Validation
{
    public static class FluentValidationExtension
    {
        public static void ValidateAndThrow<T>(this IValidator<T> validator, T @object)
        {
            FluentValidation.Results.ValidationResult validationResult = validator.Validate(@object);

            if (!validator.Validate(@object).IsValid)
            {
                throw new BusinessValidationRuleApplicationException(validationResult.Format());
            }
        }

        public static ValidationResult Format(this FluentValidation.Results.ValidationResult fluentValidationResult)
        {
            return new ValidationResult
            {
                ValidationErrors = fluentValidationResult.Errors.GroupBy(e => e.PropertyName).ToDictionary(
                    gk => gk.Key,
                    gv => gv.ToDictionary(
                            k => FormatFluentValidationErrorCode(k.ErrorCode),
                            v => v.FormattedMessagePlaceholderValues.ToDictionary(
                                mk => mk.Key,
                                mv => mv.Value
                            )
                        )
                    )
            };
        }

        private static string FormatFluentValidationErrorCode(string fluentValidationErrorCode)
        {
            return fluentValidationErrorCode.Replace("Validator", string.Empty);
        }
    }
}

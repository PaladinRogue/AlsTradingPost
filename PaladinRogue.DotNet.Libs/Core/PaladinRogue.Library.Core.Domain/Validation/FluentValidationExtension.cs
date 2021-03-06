﻿using System.Linq;
using FluentValidation;
using PaladinRogue.Library.Core.Domain.Exceptions;

namespace PaladinRogue.Library.Core.Domain.Validation
{
    public static class FluentValidationExtension
    {
        /// <summary>
        ///
        /// </summary>
        /// <param name="validator"></param>
        /// <param name="object"></param>
        /// <typeparam name="T"></typeparam>
        /// <exception cref="DomainValidationRuleException"></exception>
        public static void ValidateAndThrow<T>(this IValidator<T> validator, T @object)
        {
            FluentValidation.Results.ValidationResult validationResult = validator.Validate(@object);

            if (!validator.Validate(@object).IsValid)
            {
                throw new DomainValidationRuleException(validationResult.Format());
            }
        }

        public static ValidationResult Format(this FluentValidation.Results.ValidationResult fluentValidationResult)
        {
            return new ValidationResult
            {
                PropertyValidationErrors = fluentValidationResult.Errors.GroupBy(e => e.PropertyName).Select(g => new PropertyValidationError
                {
                    PropertyName = g.Key,
                    ValidationErrors = g.Select(e => new ValidationError
                    {
                        ValidationErrorCode = FormatFluentValidationErrorCode(e.ErrorCode),
                        ValidationMeta = e.FormattedMessagePlaceholderValues.ToDictionary(mk => mk.Key, mv => mv.Value)
                    })
                })
            };
        }

        private static string FormatFluentValidationErrorCode(string fluentValidationErrorCode)
        {
            return fluentValidationErrorCode.Replace("Validator", string.Empty);
        }
    }
}

using Common.Api.Exceptions;
using Common.Domain.Validation;

namespace Common.Api.Validation
{
    public interface IValidationErrorFormatter<out T> where T: IFormattedError
    {
        T Format(ValidationResult validationResult);
    }
}

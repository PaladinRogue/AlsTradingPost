using PaladinRogue.Libray.Core.Api.Exceptions;
using PaladinRogue.Libray.Core.Domain.Validation;

namespace PaladinRogue.Libray.Core.Api.Validation
{
    public interface IValidationErrorFormatter<out T> where T: IFormattedError
    {
        T Format(ValidationResult validationResult);
    }
}

using PaladinRogue.Library.Core.Api.Exceptions;
using PaladinRogue.Library.Core.Domain.Validation;

namespace PaladinRogue.Library.Core.Api.Validation
{
    public interface IValidationErrorFormatter<out T> where T: IFormattedError
    {
        T Format(ValidationResult validationResult);
    }
}

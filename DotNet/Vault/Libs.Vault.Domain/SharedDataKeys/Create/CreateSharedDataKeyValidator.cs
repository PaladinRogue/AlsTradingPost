using FluentValidation;
using PaladinRogue.Libray.Core.Common;

namespace PaladinRogue.Libray.Vault.Domain.SharedDataKeys.Create
{
    public class CreateSharedDataKeyValidator : AbstractValidator<CreateSharedDataKeyCommandDdto>
    {
        public CreateSharedDataKeyValidator()
        {
            RuleFor(c => c.Name)
                .MaximumLength(FieldSizes.Default)
                .NotEmpty();

            RuleFor(c => c.Value)
                .NotEmpty();
        }
    }
}
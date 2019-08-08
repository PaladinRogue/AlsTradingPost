using Common.Resources;
using FluentValidation;

namespace Vault.Domain.SharedDataKeys.Create
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
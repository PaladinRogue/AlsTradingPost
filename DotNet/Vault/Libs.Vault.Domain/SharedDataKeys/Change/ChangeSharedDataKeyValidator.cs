using FluentValidation;

namespace Vault.Domain.SharedDataKeys.Change
{
    public class ChangeSharedDataKeyValidator : AbstractValidator<ChangeSharedDataKeyCommandDdto>
    {
        public ChangeSharedDataKeyValidator()
        {
            RuleFor(c => c.Value)
                .NotEmpty();
        }
    }
}
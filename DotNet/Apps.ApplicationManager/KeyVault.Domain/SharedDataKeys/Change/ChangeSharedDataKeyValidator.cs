using FluentValidation;

namespace KeyVault.Domain.SharedDataKeys.Change
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
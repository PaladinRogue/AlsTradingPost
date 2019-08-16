using FluentValidation;

namespace PaladinRogue.Library.Vault.Domain.SharedDataKeys.Change
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
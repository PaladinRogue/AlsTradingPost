using FluentValidation;

namespace KeyVault.Domain.SharedDataKeys.Create
{
    public class CreateSharedDataKeyValidator : AbstractValidator<CreateSharedDataKeyCommandDdto>
    {
        public CreateSharedDataKeyValidator()
        {
            RuleFor(c => c.Type)
                .NotEmpty();

            RuleFor(c => c.Value)
                .NotEmpty();
        }
    }
}
using FluentValidation;

namespace ApplicationManager.Domain.Identities.Create
{
    public class CreateIdentityValidator : AbstractValidator<CreateIdentityCommandDdto>
    {
        public CreateIdentityValidator()
        {
            RuleFor(c => c)
                .NotNull();

            RuleFor(c => c.EmailAddress)
                .NotEmpty()
                .EmailAddress();
        }
    }
}
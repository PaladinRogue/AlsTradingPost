using FluentValidation;

namespace ApplicationManager.Domain.Applications
{
    public class ChangeApplicationValidator : AbstractValidator<ChangeApplicationDdto>
    {
        public ChangeApplicationValidator()
        {
            RuleFor(a => a.Name)
                .NotEmpty()
                .MaximumLength(40);
        }
    }
}

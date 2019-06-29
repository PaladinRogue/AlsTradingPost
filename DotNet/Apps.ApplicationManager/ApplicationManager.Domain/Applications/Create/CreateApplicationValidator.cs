using FluentValidation;

namespace ApplicationManager.Domain.Applications.Create
{
    public class CreateApplicationValidator : AbstractValidator<CreateApplicationDdto>
    {
        public CreateApplicationValidator()
        {
            RuleFor(a => a)
                .NotNull();

            RuleFor(a => a.SystemName)
                .NotEmpty()
                .MaximumLength(20);

            RuleFor(a => a.Name)
                .NotEmpty()
                .MaximumLength(40);
        }
    }
}

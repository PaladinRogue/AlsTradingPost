using ApplicationManager.Domain.Applications.Models;
using FluentValidation;

namespace ApplicationManager.Domain.Applications.Validators
{
    public class CreateApplicationValidator : AbstractValidator<CreateApplicationDdto>
    {
        public CreateApplicationValidator()
        {
            RuleFor(a => a.SystemName)
                .NotEmpty()
                .MaximumLength(20);

            RuleFor(a => a.Name)
                .NotEmpty()
                .MaximumLength(40);
        }
    }
}

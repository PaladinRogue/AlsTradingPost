using ApplicationManager.Domain.Applications.Models;
using FluentValidation;

namespace ApplicationManager.Domain.Applications.Validators
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

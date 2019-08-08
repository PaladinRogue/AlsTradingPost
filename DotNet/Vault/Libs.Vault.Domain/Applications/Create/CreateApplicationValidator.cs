using Common.Resources;
using FluentValidation;

namespace Vault.Domain.Applications.Create
{
    public class CreateApplicationValidator : AbstractValidator<CreateApplicationCommandDdto>
    {
        public CreateApplicationValidator()
        {
            RuleFor(a => a.SystemName)
                .NotEmpty()
                .MaximumLength(FieldSizes.Short);
        }
    }
}
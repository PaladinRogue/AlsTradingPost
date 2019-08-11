using FluentValidation;
using PaladinRogue.Libray.Core.Common;

namespace PaladinRogue.Libray.Vault.Domain.Applications.Create
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
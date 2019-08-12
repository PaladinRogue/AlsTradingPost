using FluentValidation;
using PaladinRogue.Library.Core.Common;

namespace PaladinRogue.Library.Vault.Domain.Applications.Create
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
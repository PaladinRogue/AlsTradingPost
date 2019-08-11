using FluentValidation;

namespace PaladinRogue.Libray.Vault.Domain.Applications.AddDataKey
{
    public class AddApplicationDataKeyValidator : AbstractValidator<AddApplicationDataKeyCommandDdto>
    {
        public AddApplicationDataKeyValidator()
        {
            RuleFor(a => a.Type)
                .NotEmpty();

            RuleFor(a => a.Value)
                .NotEmpty();
        }
    }
}
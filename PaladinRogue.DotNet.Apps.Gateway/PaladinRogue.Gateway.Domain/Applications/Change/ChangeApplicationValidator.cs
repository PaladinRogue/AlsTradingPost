using FluentValidation;
using PaladinRogue.Library.Core.Common;

namespace PaladinRogue.Gateway.Domain.Applications.Change
{
    public class ChangeApplicationValidator : AbstractValidator<ChangeApplicationDdto>
    {
        public ChangeApplicationValidator()
        {
            RuleFor(a => a)
                .NotNull();

            RuleFor(a => a.Name)
                .NotEmpty()
                .MaximumLength(40);

            RuleFor(a => a.HostUri)
                .NotEmpty()
                .MaximumLength(FieldSizes.Extended)
                .Matches(RegexPatterns.HttpsUri);
        }
    }
}

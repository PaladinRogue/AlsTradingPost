using Common.Resources;
using FluentValidation;

namespace Gateway.Domain.Applications.Create
{
    public class CreateApplicationValidator : AbstractValidator<CreateApplicationDdto>
    {
        public CreateApplicationValidator()
        {
            RuleFor(a => a)
                .NotNull();

            RuleFor(a => a.SystemName)
                .NotEmpty()
                .MaximumLength(FieldSizes.Short);

            RuleFor(a => a.Name)
                .NotEmpty()
                .MaximumLength(FieldSizes.Default);

            RuleFor(a => a.HostUri)
                .NotEmpty()
                .MaximumLength(FieldSizes.Extended)
                .Matches(RegexPatterns.HttpsUri);
        }
    }
}

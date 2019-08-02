using Common.Resources;
using FluentValidation;

namespace Authentication.Domain.Identities.Login.RefreshToken
{
    public class RefreshTokenLoginCommandValidator : AbstractValidator<RefreshTokenLoginCommandDdto>
    {
        public RefreshTokenLoginCommandValidator()
        {
            RuleFor(r => r)
                .NotNull();

            RuleFor(l => l.SessionId)
                .NotEmpty();

            RuleFor(l => l.RefreshToken)
                .MaximumLength(FieldSizes.Extended)
                .NotEmpty();
        }
    }
}
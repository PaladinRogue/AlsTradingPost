using Common.Resources;
using FluentValidation;

namespace ApplicationManager.Domain.Identities.Login.RefreshToken
{
    public class RefreshTokenLoginCommandValidator : AbstractValidator<RefreshTokenLoginCommandDdto>
    {
        public RefreshTokenLoginCommandValidator()
        {
            RuleFor(l => l.SessionId)
                .NotEmpty();

            RuleFor(l => l.RefreshToken)
                .MaximumLength(FieldSizes.Extended)
                .NotEmpty();
        }
    }
}
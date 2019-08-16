using FluentValidation;
using PaladinRogue.Library.Core.Common;

namespace PaladinRogue.Authentication.Domain.Identities.Login.RefreshToken
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
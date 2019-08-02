using FluentValidation;

namespace Authentication.Domain.Identities.ForgotPassword
{
    public class ForgotPasswordCommandValidator : AbstractValidator<ForgotPasswordCommandDdto>
    {
        public ForgotPasswordCommandValidator()
        {
            RuleFor(f => f)
                .NotNull();

            RuleFor(f => f.EmailAddress)
                .NotEmpty()
                .EmailAddress();
        }
    }
}
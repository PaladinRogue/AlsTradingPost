using FluentValidation;

namespace ApplicationManager.Domain.Identities.ForgotPassword
{
    public class ForgotPasswordCommandValidator : AbstractValidator<ForgotPasswordCommandDdto>
    {
        public ForgotPasswordCommandValidator()
        {
            RuleFor(f => f.EmailAddress)
                .NotEmpty()
                .EmailAddress();
        }
    }
}
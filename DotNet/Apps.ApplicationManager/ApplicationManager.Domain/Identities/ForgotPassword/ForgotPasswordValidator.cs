using FluentValidation;

namespace ApplicationManager.Domain.Identities.ForgotPassword
{
    public class ForgotPasswordValidator : AbstractValidator<ForgotPasswordCommandDdto>
    {
        public ForgotPasswordValidator()
        {
            RuleFor(f => f.EmailAddress)
                .NotEmpty()
                .EmailAddress();
        }
    }
}
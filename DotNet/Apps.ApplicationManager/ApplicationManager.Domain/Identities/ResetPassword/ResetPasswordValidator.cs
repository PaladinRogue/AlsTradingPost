using System.Text.RegularExpressions;
using Common.Resources;
using FluentValidation;

namespace ApplicationManager.Domain.Identities.ResetPassword
{
    public class ResetPasswordValidator : AbstractValidator<ResetPasswordCommandDdto>
    {
        public ResetPasswordValidator()
        {
            Regex passwordRegex = new Regex(RegexPatterns.Password);

            RuleFor(i => i.Token)
                .NotEmpty();

            RuleFor(i => i.Password)
                .NotEmpty()
                .Length(6, 80)
                .Matches(passwordRegex);

            RuleFor(i => i.ConfirmPassword)
                .NotEmpty()
                .Length(6, 80)
                .Equal(i => i.Password);
        }
    }
}
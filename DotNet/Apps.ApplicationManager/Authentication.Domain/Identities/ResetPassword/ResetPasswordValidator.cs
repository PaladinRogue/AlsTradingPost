using System.Text.RegularExpressions;
using FluentValidation;

namespace PaladinRogue.Authentication.Domain.Identities.ResetPassword
{
    public class ResetPasswordValidator : AbstractValidator<ResetPasswordCommandDdto>
    {
        public ResetPasswordValidator()
        {
            RuleFor(r => r)
                .NotNull();

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
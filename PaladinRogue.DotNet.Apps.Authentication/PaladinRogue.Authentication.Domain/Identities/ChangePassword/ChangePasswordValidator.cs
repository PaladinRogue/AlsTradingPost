using System.Text.RegularExpressions;
using FluentValidation;

namespace PaladinRogue.Authentication.Domain.Identities.ChangePassword
{
    public class ChangePasswordValidator : AbstractValidator<ChangePasswordCommandDdto>
    {
        public ChangePasswordValidator()
        {
            RuleFor(c => c)
                .NotNull();

            Regex passwordRegex = new Regex(RegexPatterns.Password);

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
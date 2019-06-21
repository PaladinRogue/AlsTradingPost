using System.Text.RegularExpressions;
using ApplicationManager.Domain.Identities.AddConfirmedPassword;
using Common.Resources;
using FluentValidation;

namespace ApplicationManager.Domain.Identities.ChangePassword
{
    public class ChangePasswordValidator : AbstractValidator<ChangePasswordDdto>
    {
        public ChangePasswordValidator()
        {
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
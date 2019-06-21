using System.Text.RegularExpressions;
using ApplicationManager.Domain.Identities.AddConfirmedPassword;
using Common.Resources;
using FluentValidation;

namespace ApplicationManager.Domain.Identities.RegisterPassword
{
    public class RegisterPasswordValidator : AbstractValidator<RegisterPasswordDdto>
    {
        public RegisterPasswordValidator()
        {
            Regex passwordRegex = new Regex(RegexPatterns.Password);

            RuleFor(i => i.Identifier)
                .NotEmpty()
                .Length(6, 80);

            RuleFor(i => i.Password)
                .NotEmpty()
                .Length(6, 80)
                .Matches(passwordRegex);

            RuleFor(i => i.ConfirmPassword)
                .NotEmpty()
                .Length(6, 80)
                .Equal(i => i.Password);

            RuleFor(i => i.EmailAddress)
                .NotEmpty()
                .EmailAddress();
        }
    }
}
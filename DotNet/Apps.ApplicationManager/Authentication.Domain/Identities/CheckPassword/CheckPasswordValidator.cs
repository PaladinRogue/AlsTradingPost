using System.Text.RegularExpressions;
using Common.Resources;
using FluentValidation;

namespace Authentication.Domain.Identities.CheckPassword
{
    public class CheckPasswordValidator : AbstractValidator<CheckPasswordDdto>
    {
        public CheckPasswordValidator()
        {
            RuleFor(c => c)
                .NotNull();

            Regex passwordRegex = new Regex(RegexPatterns.Password);

            RuleFor(c => c)
                .NotNull();

            RuleFor(c => c.Password)
                .NotEmpty()
                .Matches(passwordRegex);
        }
    }
}
using System.Text.RegularExpressions;
using ApplicationManager.Domain.Identities.Models;
using Common.Resources;
using FluentValidation;

namespace ApplicationManager.Domain.Identities.Validators
{
    public class AddConfirmedPasswordIdentityValidator : AbstractValidator<AddConfirmedPasswordIdentityDdto>
    {
        public AddConfirmedPasswordIdentityValidator()
        {
            Regex passwordRegex = new Regex(RegexPatterns.Password);

            RuleFor(i => i.Identifier)
                .NotEmpty()
                .Length(6, 80);

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
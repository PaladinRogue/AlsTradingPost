using Common.Resources;
using FluentValidation;

namespace Authentication.Domain.AuthenticationServices.ChangeFacebook
{
    public class ChangeAuthenticationGrantTypeFacebookValidator : AbstractValidator<ChangeAuthenticationGrantTypeFacebookDdto>
    {
        public ChangeAuthenticationGrantTypeFacebookValidator()
        {
            RuleFor(a => a)
                .NotNull();

            RuleFor(a => a.Name)
                .NotEmpty()
                .MaximumLength(FieldSizes.Default);

            RuleFor(a => a.ClientId)
                .NotEmpty()
                .MaximumLength(FieldSizes.Default);

            RuleFor(a => a.ClientSecret)
                .NotEmpty()
                .MaximumLength(FieldSizes.Default);

            RuleFor(a => a.ClientGrantAccessTokenUrl)
                .NotEmpty()
                .MaximumLength(FieldSizes.Extended);

            RuleFor(a => a.GrantAccessTokenUrl)
                .NotEmpty()
                .MaximumLength(FieldSizes.Extended);

            RuleFor(a => a.ValidateAccessTokenUrl)
                .NotEmpty()
                .MaximumLength(FieldSizes.Extended);

            RuleFor(a => a.AppAccessToken)
                .NotEmpty()
                .MaximumLength(FieldSizes.Extended);
        }
    }
}
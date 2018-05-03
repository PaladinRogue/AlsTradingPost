using Authentication.Application.Authentication.Models;
using FluentValidation;

namespace Authentication.Application.Authentication.Validators
{
    public class LoginValidator : AbstractValidator<LoginAdto>
    {
        public LoginValidator()
        {
            RuleFor(r => r.AccessToken)
                .NotNull()
                .NotEmpty();
            
            RuleFor(r => r.AuthenticationId)
                .NotNull()
                .NotEmpty();
        }
    }
}
using AlsTradingPost.Application.Authentication.Models;
using FluentValidation;

namespace AlsTradingPost.Application.Authentication.Validators
{
    public class LoginValidator : AbstractValidator<LoginAdto>
    {
        public LoginValidator()
        {
        }
    }
}
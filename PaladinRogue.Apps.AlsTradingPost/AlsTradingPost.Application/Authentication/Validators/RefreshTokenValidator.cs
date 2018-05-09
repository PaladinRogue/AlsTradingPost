using System;
using AlsTradingPost.Application.Authentication.Models;
using FluentValidation;

namespace AlsTradingPost.Application.Authentication.Validators
{
    public class RefreshTokenValidator : AbstractValidator<RefreshTokenAdto>
    {
        public RefreshTokenValidator()
        {
            RuleFor(r => r.SessionId)
                .NotNull()
                .NotEqual(Guid.Empty);
            
            RuleFor(r => r.Token)
                .NotNull()
                .NotEmpty();
        }
    }
}
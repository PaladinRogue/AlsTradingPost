using System;
using Authentication.Application.Authentication.Models;
using FluentValidation;

namespace Authentication.Application.Authentication.Validators
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
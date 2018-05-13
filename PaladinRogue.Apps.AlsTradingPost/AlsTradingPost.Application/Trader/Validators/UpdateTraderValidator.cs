using System;
using AlsTradingPost.Application.Trader.Models;
using FluentValidation;

namespace AlsTradingPost.Application.Trader.Validators
{
    public class UpdateTraderValidator : AbstractValidator<UpdateTraderAdto>
    {
        public UpdateTraderValidator()
        {
            RuleFor(x => x.Id)
                .NotNull()
                .NotEqual(Guid.Empty);

            RuleFor(x => x.Alias)
                .MaximumLength(50)
                .NotEmpty();
            
            RuleFor(x => x.DCI)
                .MaximumLength(50)
                .NotEmpty();
        }
    }
}